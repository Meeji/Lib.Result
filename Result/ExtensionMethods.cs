namespace Result;

using System;
using System.Collections.Generic;
using System.Linq;
using Unsafe;

public static class ExtensionMethods
{
    public static async Task<TOutput> DoAsync<TSuccess, TFailure, TOutput>(
        this Task<Result<TSuccess, TFailure>> task, 
        Func<TSuccess, TOutput> onSuccess,
        Func<TFailure, TOutput> onFailure) =>
        (await task).Do(onSuccess, onFailure);
    
    public static async Task<TOutput> DoAsync<TSuccess, TFailure, TOutput>(
        this Task<Result<TSuccess, TFailure>> task, 
        Func<TSuccess, Task<TOutput>> onSuccess,
        Func<TFailure, Task<TOutput>> onFailure) =>
        await (await task).Do(onSuccess, onFailure);

    public static IEnumerable<T> SelectSuccess<T, TF>(this IEnumerable<Result<T, TF>> values) 
        => values.Where(a => a.IsSuccess).Select(a => a.Unwrap());

    public static IEnumerable<TF> SelectFailure<T, TF>(this IEnumerable<Result<T, TF>> values) 
        => values.Where(a => a.IsFailure).Select(a => a.UnwrapError());

    public static Result<TValue, CollectionError> TryGetValueAsResult<TKey, TValue>(this IDictionary<TKey, TValue>? dict, TKey key)
    {
        if (dict == null)
        {
            return CollectionError.IsNull;
        }

        if (dict.TryGetValue(key, out var outValue))
        {
            return outValue;
        }

        return CollectionError.NoMatchingItems;
    }

    public static Result<T, CollectionError> SingleAsResult<T>(this IQueryable<T>? values)
    {
        if (values == null)
        {
            return CollectionError.IsNull;
        }

        // To make SingleAsResult efficient when the enumerable underlying it is a database, we attempt to take 2 then see if there are two elements
        // Take(2) should send 'SELECT TOP(2) *' to the database, instead of 'SELECT *' if we use an underlying enumerable
        var list = values.Take(2).ToList();
        return list.Count switch
        {
            2 => CollectionError.MultipleMatchingItems,
            0 => CollectionError.IsEmpty,
            _ => list[0]
        };
    }

    public static Result<T, CollectionError> SingleAsResult<T>(this IEnumerable<T>? values)
    {
        if (values == null)
        {
            return CollectionError.IsNull;
        }

        using var e = values.GetEnumerator();
        if (!e.MoveNext())
        {
            return CollectionError.IsEmpty;
        }

        var result = e.Current;
        if (!e.MoveNext())
        {
            return result;
        }

        return CollectionError.MultipleMatchingItems;
    }
    
    public static Result<T, CollectionError> SingleAsResult<T>(this IEnumerable<T>? values, Func<T, bool> predicate)
    {
        if (values == null)
        {
            return CollectionError.IsNull;
        }
        
        using var e = values.GetEnumerator();
        if (!e.MoveNext())
        {
            return CollectionError.IsEmpty;
        }
        
        var itemFound = predicate(e.Current);
        var item = e.Current;

        while (e.MoveNext())
        {
            if (predicate(e.Current))
            {
                if (itemFound)
                {
                    return CollectionError.MultipleMatchingItems;
                }

                itemFound = true;
                item = e.Current;
            }
        }

        if (itemFound)
        {
            return item;
        }
        
        return CollectionError.NoMatchingItems;
    }
    
    /// <summary>
    /// Flattens two nested results with matching Failure type into a single Result
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="TSuccess"></typeparam>
    /// <typeparam name="TFailure"></typeparam>
    /// <returns></returns>
    public static Result<TSuccess, TFailure> Flatten<TSuccess, TFailure>(
        this Result<Result<TSuccess, TFailure>, TFailure> result) => result.Then(t => t);

    /// <summary>
    /// Flattens a enumerable of Result`TS, TF` into a Result`IEnumerable`TS`, TF`
    /// </summary>
    /// <param name="results"></param>
    /// <typeparam name="TSuccess"></typeparam>
    /// <typeparam name="TFailure"></typeparam>
    /// <returns></returns>
    public static Result<IEnumerable<TSuccess>, TFailure> Flatten<TSuccess, TFailure>(
        this IEnumerable<Result<TSuccess, TFailure>> results)
    {
        var list = new List<TSuccess>();

        foreach (var result in results)
        {
            if (result is ISuccess<TSuccess> (var success))
            {
                list.Add(success);
            }
            else
            {
                return result.UnwrapError();
            }
        }

        return list;
    }
    
    /// <summary>
    /// Flattens a enumerable of Result`TS, TF` into a Result`IEnumerable`TS`, TF`
    /// </summary>
    /// <param name="results"></param>
    /// <typeparam name="TSuccess"></typeparam>
    /// <typeparam name="TFailure"></typeparam>
    /// <returns></returns>
    public static async Task<Result<IEnumerable<TSuccess>, TFailure>> FlattenAsync<TSuccess, TFailure>(
        this IEnumerable<Task<Result<TSuccess, TFailure>>> results)
    {
        var list = new List<TSuccess>();

        foreach (var resultTask in results)
        {
            var result = await resultTask;
            if (result is ISuccess<TSuccess> (var success))
            {
                list.Add(success);
            }
            else
            {
                return result.UnwrapError();
            }
        }

        return list;
    }

    public static Result<TSuccess, TFailureNew> ChangeFailure<TSuccess, TFailure, TFailureNew>(
        this Result<TSuccess, TFailure> result, TFailureNew newValue) 
        => result.Then(success => success, _ => newValue);

    public static Result<TSuccess, TFailureNew> ChangeFailure<TSuccess, TFailure, TFailureNew>(
        this Result<TSuccess, TFailure> result,
        Func<TFailureNew> newValue) => result.Then(success => success, _ => newValue());

    public static Result<TSuccess, TFailureNew> ChangeFailure<TSuccess, TFailure, TFailureNew>(
        this Result<TSuccess, TFailure> result,
        Func<TFailure, TFailureNew> newValue) => result.Do<Result<TSuccess, TFailureNew>>(
        success => success, 
        failure => newValue(failure));

    public static T Either<T>(this Result<T, T> result) => result.Do(t => t, t => t);

    public static object? Either<TSuccess, TFailure>(this Result<TSuccess, TFailure> result) 
        => result.Do<object?>(t => t, t => t);

    /// <summary>
    /// Tests the inner Success value against a predicate. If it passes, a new Result is created retaining the value
    /// If it fails, a new Result.Failure is created with the given failure value
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="replaceWith">The value that replaces a Success if it fails the predicate.</param>
    /// <typeparam name="TSuccess"></typeparam>
    /// <typeparam name="TFailure"></typeparam>
    /// <returns>A new Result which retains the </returns>
    public static Result<TSuccess, TFailure> RetainIf<TSuccess, TFailure>(
        this Result<TSuccess, TFailure> result, 
        Func<TSuccess, bool> predicate, 
        TFailure replaceWith) 
        => result.Then(s => predicate(s) ? Result.Success<TSuccess, TFailure>(s) : replaceWith);

    /// <summary>
    /// Tests the inner Success value against a predicate. If it passes, a new Result is created retaining the value
    /// If it fails, a new Result.Failure is created with the given failure value
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="replaceFunc">Func to generate a value to replace the Success that failed the preicate</param>
    /// <typeparam name="TSuccess"></typeparam>
    /// <typeparam name="TFailure"></typeparam>
    /// <returns>A new Result which retains the </returns>
    public static Result<TSuccess, TFailure> RetainIf<TSuccess, TFailure>(
        this Result<TSuccess, TFailure> result, 
        Func<TSuccess, bool> predicate, 
        Func<TSuccess, TFailure> replaceFunc) 
        => result.Then(s => predicate(s) ? Result.Success<TSuccess, TFailure>(s) : replaceFunc(s));

    /// <summary>
    /// Tests the inner Success value against a func which returns a Result type.
    /// If the return value of func is a Success, a new Result is created which retains the value of the original Result.
    /// If the result value is a failure, a new Result is created containing the failure value of func.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="func">The test function.</param>
    /// <typeparam name="TSuccess"></typeparam>
    /// <typeparam name="TFailure"></typeparam>
    /// <typeparam name="TNew"></typeparam>
    /// <returns></returns>
    public static Result<TSuccess, TFailure> RetainIf<TSuccess, TNew, TFailure>(
        this Result<TSuccess, TFailure> result, 
        Func<TSuccess, Result<TNew, TFailure>> func) 
        => result.Then(s => func(s).Then(_ => s));
    
    public static Task<Result<TSuccess, TFailure>> RetainIfAsync<TSuccess, TNew, TFailure>(
        this Result<TSuccess, TFailure> result,
        Func<TSuccess, Task<Result<TNew, TFailure>>> func)
        => result.ThenAsync(async s => (await func(s)).Then(_ => s));

    public static Result<TSuccess, TFailure> RetainNotNull<TSuccess, TFailure>(this Result<TSuccess?, TFailure> result, TFailure replaceWith)
    where TSuccess : class => result.Then(s => s is null ? replaceWith : Result.Success<TSuccess, TFailure>(s));
    
    public static Task<Result<TSuccess, TFailure>> RetainNotNullAsync<TSuccess, TFailure>(this Task<Result<TSuccess?, TFailure>> result, TFailure replaceWith)
        where TSuccess : class => result.ThenAsync(s => 
        Task.FromResult(s is null ? replaceWith : Result.Success<TSuccess, TFailure>(s)));

    public static void OnSuccess<TSuccess, TFailure>(this Result<TSuccess, TFailure> result, Action<TSuccess> action) 
        => result.Do(action, _ => { });

    public static void OnFailure<TSuccess, TFailure>(this Result<TSuccess, TFailure> result, Action<TFailure> action) 
        => result.Do(_ => { }, action);

    public static bool TryGetSuccess<TSuccess, TFailure>(this Result<TSuccess, TFailure> result, out TSuccess value)
    {
        if (result is ISuccess<TSuccess>(var success))
        {
            value = success;
            return true;
        }

        value = default!;
        return false;
    }

    public static bool TryGetFailure<TSuccess, TFailure>(this Result<TSuccess, TFailure> result, out TFailure value)
    {
        if (result is IFailure<TFailure>(var failure))
        {
            value = failure;
            return true;
        }

        value = default!;
        return false;
    }

    public static Result<TNew, TFailure> Replace<TNew, TSuccess, TFailure>
        (this Result<TSuccess, TFailure> result, TNew value)
        => result.Then(_ => value);

    public static Result<TNew, TFailure> Aggregate<TNew, TSuccess, TFailure>(
        this Result<IEnumerable<TSuccess>, TFailure> result,
        TNew seed,
        Func<TSuccess, Result<TNew, TFailure>> func,
        Func<TNew, TNew, TNew> aggregator = null!) =>
        result.Then(inners =>
        {
            aggregator ??= ((i, j) => j);
            var last = seed;

            foreach (var inner in inners)
            {
                var funcResult = func(inner);
                if (funcResult is IFailure<TFailure> fail)
                {
                    return (Result<TNew, TFailure>)fail;
                }

                last = aggregator(last, ((ISuccess<TNew>)funcResult).Value);
            }

            return last;
        });
    
    public static Task<Result<TNew, TFailure>> AggregateAsync<TNew, TSuccess,  TFailure>(
        this Result<IEnumerable<TSuccess>, TFailure> result,
        TNew seed,
        Func<TSuccess, Task<Result<TNew, TFailure>>> func,
        Func<TNew, TNew, TNew> aggregator = null!) =>
        result.ThenAsync(async inners =>
        {
            aggregator ??= ((i, j) => j);
            var last = seed;

            foreach (var inner in inners)
            {
                var funcResult = await func(inner);
                if (funcResult is IFailure<TFailure> fail)
                {
                    return (Result<TNew, TFailure>)fail;
                }

                last = aggregator(last, ((ISuccess<TNew>)funcResult).Value);
            }

            return last;
        });
}
