[![Build status](https://ci.appveyor.com/api/projects/status/wl77l62ewkeumh5s/branch/master?svg=true)](https://ci.appveyor.com/project/Meeji/lib-result/branch/master)

# Result
C# type that represents either the result of a task that may succeed, or the task itself. Similar to Result types in functional languages.

In some cases a method can be reasonably expected to fail - for instance if it attempts to read a file that may not exist, or accesses a network resource that may not be available. In these cases C# offers several patterns to deal with that method, but these are often verbose or unsuitable for many use cases. Consider returning a ```Result```!

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

Once we've got a ```Result``` type in hand it can be handled in several ways. The result type forces us to explicitly handle the possibility of an error state (even if we only handle it by saying 'assume success or throw'):

```csharp
// Give the type an Action to perform on success, and another on failure
var customerResult = GetCustomer(10);
customerResult.Do(
    onSuccess: customer => Console.WriteLine($"Customer name is: {customer.Name}"),
    onFailure: error    => Console.WriteLine($"Error! {error}"));

// If passed Funcs the results can be assigned:
var gotCustomer = customerResult.Do(
    customer => true,
    error    => false);

// There is a shortcut for the example above:
gotCustomer = customerResult.IsSuccess;
gotCustomer = !customerResult.IsFailure;

// We can assume success and attempt to unwrap the inner value.
// This will throw if our assumption is incorrect.
var customer = customerResult.Unwrap();

// To be a bit safer we can provide a default value - in this case null
customer = customerResult.Or(null);

// Or provide the default through a func
customer = customerResult.Or(() => new Customer);

// Or safely retrieve the customer through pattern matching
if (customerResult is Success<Customer, string>(var c))
{
    customer = c;
}

// We can also assume there was an error.
// This unwraps the 'failure' value or throws if the result represents a success.
var error = customerResult.UnwrapError();
```

## ```Result``` workflow
The ```Result``` type allows you to 'use' the inner value without unwrapping it using several ```Bind``` methods.

```csharp
var five = 5;
var successFive = (Result<int, string>)five; // Could also have used 'Result.Success<int, string>(five)';
var successTen = successFive.Bind(i => i + 5); // Value is mutated without unwrapping
successTen.Unwrap(); // 10!

var failureFive = (Result<int, string>)"some error"; // 'Result.Failure<int, string>("some error")';
var failureTen = failureFive.Bind(i => i + 5); // Value is mutated without unwrapping
failureTen.UnwrapError(); // "some error"!
```

But why is that helpful? It creates a nice workflow when you're working with chains of methods that might want to do work with a value, but might also fail.
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
public static IList<Order> GetOrdersForCustomer(int id) {
    // BindToResult works like Bind, but expects a Result<TSuccess, TFailure>
    // where TFailure matches with the current Result
    return GetCustomer(id)        // We have a Result<Customer, TFailure>
        .MapToResult(GetAddress) // Now a Result<Address, TFailure>
        .MapToResult(GetOrders)  // And now a Result<IList<Orders>, TFailure>
        .Or(error => /* Handle error case */); // Handle TFailure in error case
}
```

This compresses the code by removing a lot of boilerplate and moves the error handling to a single place. This style of pushing errors down to the end of the method and handling them there has been called 'railway oriented programming' and Scott Wlaschin did a brilliant talk on it you can find [here](https://vimeo.com/113707214) if you're interested.

## Pattern Matching
Both `Success` and `Failure` types can be deconstructed to retrieve their inner values:

```csharp
if (result is Failure<int, Exception>(var exc))
{
    throw new Exception("Oh no!", exc);
}
```

```csharp
var numberString = result switch
{
    Success<int, Exception>(var success) when success is <= 5         => "a small number.",
    Success<int, Exception>(var success) when success is > 5 and < 10 => "a medium number",
    Success<int, Exception>                                           => "a large number",
    Failure<int, Exception>(var exc)                                  => throw exc
};
```