[![Build status](https://ci.appveyor.com/api/projects/status/wl77l62ewkeumh5s/branch/master?svg=true)](https://ci.appveyor.com/project/Meeji/lib-result/branch/master)

# Result
C# type that represents either the result of a task that may succeed, or the task itself. Similar to Result types in functional languages.

In some cases a method can be reasonably expected to fail - for instance if it attempts to read a file that may not exist, or accesses a network resource that may not be available. In these cases C# offers several patterns to deal with that method, but these are often verbose or unsuitable for many use cases. Consider returning a `Result`!

## By Example
Consider the following method definition, which emulates a database call with a 1% chance of failing:

```csharp
// Return a Result that represents either a Customer object on success, or a string on failure.
public static Result<Customer, string> GetCustomer(int id)
{
    if (new Random().Next(100) != 1)
    {
        return new Customer { Id = id, Name = "John Smith" };
    }

    return "Could not retrieve customer";
}
```

Once we've got a `Result` type in hand it can be handled in several ways. We can apply continuations to the resultThe result type forces us to explicitly handle the possibility of an error state (even if we only handle it by saying 'assume success or throw'):

```csharp
var result = GetCustomer(10)                // a Result<Customer, string>
    .RetainIf(c => c.IsActive, "Inactive")  // Continuation: Filter inner value
    .Then(c => GetOrders(c));               // Continuation: Do work using inner value

// Resolve: Give an Action to perform on success, and another on failure
result.Do(
    onSuccess: customer => Console.WriteLine($"Customer name is: {customer.Name}"),
    onFailure: error    => Console.WriteLine($"Error! {error}"));

// If passed Funcs the results can be assigned:
var gotCustomerOrders = result.Do(
    customer => true,
    error    => false);

// There is a shortcut for the example above:
gotCustomerOrders = result.IsSuccess; // And matching IsFailure

// We can assume success and attempt to unwrap the inner value.
// This will throw if our assumption is incorrect.
// Operations which might throw an exception are in the Unsafe namespace.
var value = result.Unwrap();

// To be a safer we can provide a default value - in this case null
value = result.Or(null);

// Or provide the default through a func
value = result.Or(() => GetDefault());

// Or safely retrieve the customer through pattern matching
if (result is ISuccess<Order>(var o))
{
    value = o;
}

// We can also assume there was an error.
// This unwraps the 'failure' value or throws if the result represents a success.
var error = customerResult.UnwrapError();
```

## `Result` workflow
The `Result` type allows you to 'use' the inner value without unwrapping it using `Then`.

```csharp
var five = 5;
var successFive = (Result<int, string>)five;   // Could also have used 'Result.Success<int, string>(five)' or 'new Success<int, string>(five)';
var successTen = successFive.Then(i => i + 5); // Value is mutated without unwrapping
if (successTen is ISuccess<int>(var value))
{
    Console.WriteLine($"Our value is now {value}!")
}

var failureFive = (Result<int, string>)"some error";
var failureTen = failureFive.Then(i => i + 5); // Value is mutated without unwrapping
failureTen.UnwrapError(); // "some error"!
```

This creates a nice workflow when you're working with chains of methods that might want to do work with a value, but might also fail.
Consider the (highly contrived) example below where you must get a customer, then the customer's address, then orders for that address. The resulting method might look like:

```csharp
public static IList<Order> GetOrdersForCustomer(int id) {
    Customer customer;
    try {
        customer = GetCustomer(id);
    }
    catch(Exception ex) {
        // Handle error case
    }

    Address address;
    try {
        address = GetAddress(customer);
    }
    catch(Exception ex) {
        // Handle error case
    }

    IList<Order> orders;
    try {
        orders = GetOrders(address);
    }
    catch(Exception ex) {
        // Handle error case
    }

    return orders;
}
```

Yes, I know. You wouldn't actually write code like that. But it illustrates how error handling often works in C#: there are multiple exit points from a method, most of them dealing with the many ways a method like this can fail. If each of these methods returned a ```Result<TSuccess, TFailure>``` type with a common ```TFailure``` (say a RetrievalError DO, an Exception, or an Enum), the code could be simplified to:

```csharp
public static IList<Order> GetOrdersForCustomer(int id) =>
    GetCustomer(id)       // Start flow: We have a Result<Customer, TFailure>
        .Then(GetAddress) // Continuation: Now a Result<Address, TFailure>
        .Then(GetOrders)  // Continuation: And now a Result<IList<Orders>, TFailure>
        .Or(error => /* Handle error case */); // Handle TFailure in error case
```

This compresses the code by removing a lot of boilerplate and moves the error handling to a single place. This style of pushing errors down to the end of the method and handling them there has been called 'railway oriented programming' and Scott Wlaschin did a talk on it you can find [here](https://vimeo.com/113707214) which is a good explanation of the pattern.

## Pattern Matching
Both `Success` and `Failure` types can be deconstructed to retrieve their inner values:

```csharp
if (result is Failure<int, Exception>(var exc))
{
    throw exc;
}
```

For convenience `Success<T1, T2>` implements `ISuccess<T1>` and `Failure<T1, T2>` implements `IFailure<T2>`

```csharp
if (result is IFailure<Exception>(var exc))
{
    throw exc;
}
```

Though the previous example does have a shortcut method in the `Unsafe` namespace:

```csharp
result.OrThrow();
```

Another example:

```csharp
var numberString = result switch
    {
        ISuccess<int>(<= 5)         => "a small number.",
        ISuccess<int>(> 5 and < 10) => "a medium number",
        ISuccess<int>               => "a large number",
        IFailure<string>(var e)     => $"no number because {e} :("
    };
```

## Async
Many extension methods have async versions that work the same way with a `Task<Result<T1, T2>>` that the synchronous version does on a `Result<T1, T2>`. This allows you to transparently work with a running task as if it were a value.

```csharp
var value = await GetCustomerAsync()       // Start async workflow
    .RetainNotNullAsync("Unexpected null") // Continuation: filter
    .ThenAsync(c => GetOrdersAsync(c))     // A continuation
    .DoAsync(                              // A resolution
        orders => ProcessOrders(orders),    
        error => HandleError(error));
```

## Helpers

There are quite a few helper methods to make working with Result workflows easier. Here are a few examples:

### `IEnumerable` helpers

```csharp
List<Result<int, string>> results = [1, 2, "failed!", 4];

var aggregation = results
    .SelectSuccess()      // Just the success values
    .Flatten()            // Converts from IEnumerable<Result<T1, T2>> to Result<IEnumerable<T1>, T2>
    .Aggregate(           // Aggregate the success values
        0,                // Seed value of 0
        i => i * 2,       // Multiply every inner value by 2
        (i, j) => i + j); // And add them all together.

Debug.Assert(aggregation is ISuccess<int>(14));
```

### RetainIf

```csharp
Result<int, string> KeepEvenNumbers(this Result<int?, string> result) => result
    .RetainNotNull("NULL value rejected")
    .RetainIf(i => i % 2 == 0, i => $"{i} rejected for not being an even number.");
```

### Working with Multiple Results

It can be a bit awkward to work with a lot of result. For instance, if you have a method which takes args `T1`, `T2`, `T3`, `T4`, `T5` and a corrosponding `result1` to `resultN` of type `Result<TN, TFailure>` how would you call that method? The basic way would look like:

```csharp
result.Then(r1 =>
    result2.Then(r2 =>
        result3.Then(r3 =>
            result4.Then(r4 =>
                result5.Then(r5 =>
                    MethodCall(r1, r2, r3, r4, r5))))));
```

Alternatively, you could `Unwrap` each result, but it's best to avoid unsafe. We have some extension methods that help with this case: `And` allows multiple `Result` values to be packed together, and then a special form of the continuation method `Then` can be used to call the method:

```csharp
result
    .And(result2, result3, result4, result5)                       // Packs the results into a Result<(T1, T2, T3, T4, T5), TFailure>
    .Then((r1, r2, r3, r4, r5) => MethodCall(r1, r2, r3, r4, r5)); // Continuation

result.And(result2, result3, result4, result5).Then(MethodCall);   // Also works
```
