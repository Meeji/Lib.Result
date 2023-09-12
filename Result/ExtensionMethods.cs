namespace Result;

using System;
using System.Collections.Generic;
using System.Linq;

public static class ExtensionMethods
{
    public static IEnumerable<T> SelectSuccess<T, TF>(this IEnumerable<Result<T, TF>> values) => values.Where(a => a.IsSuccess).Select(a => a.Unwrap());

    public static IEnumerable<TF> SelectFailure<T, TF>(this IEnumerable<Result<T, TF>> values) => values.Where(a => a.IsFailure).Select(a => a.UnwrapError());

    public static Result<IEnumerable<TSuccess>, TFailure> UnwrapAll<TSuccess, TFailure>(this IEnumerable<Result<TSuccess, TFailure>> values)
    {
        var outList = new List<TSuccess>();
        foreach (var val in values)
        {
            if (val.IsSuccess)
            {
                outList.Add(val.Unwrap());
            }
            else
            {
                return val.UnwrapError();
            }
        }

        return outList;
    }

    public static Result<TValue, string> TryGetValueAsResult<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
    {
        if (dict == null)
        {
            return "Could not get value from null dictionary";
        }

        if (dict.TryGetValue(key, out var outValue))
        {
            return outValue;
        }

        return string.Format("Dictionary does not contain key: {0}", key);
    }

    public static Result<T, string> SingleAsResult<T>(this IQueryable<T> values)
    {
        if (values == null)
        {
            return "SingleAsResult called on null collection";
        }

        // To make SingleAsResult efficient when the enumerable underlying it is a database, we attempt to take 2 then see if there are two elements
        // Take(2) should send 'SELECT TOP(2) *' to the database, instead of 'SELECT *' if we use an underlying enumerable
        var list = values.Take(2).ToList();
        if (list.Count == 2)
        {
            return "SingleAsResult called on collection with more than one element";
        }

        if (list.Count == 0)
        {
            return "SingleAsResult called on collection with no elements";
        }

        return list[0];
    }

    public static Result<T, string> SingleAsResult<T>(this IEnumerable<T> values)
    {
        if (values == null)
        {
            return "SingleAsResult called on null collection";
        }

        using (var e = values.GetEnumerator())
        {
            if (!e.MoveNext())
            {
                return "SingleAsResult called on collection with no elements";
            }

            var result = e.Current;
            if (!e.MoveNext())
            {
                return result;
            }

            return "SingleAsResult called on collection with more than one element";
        }
    }

    public static TSuccess UnwrapOrThrow<TSuccess, TFailure>(this Result<TSuccess, TFailure> result)
        where TFailure : Exception => result.Or(exc => { throw exc; });

    public static Result<TSuccess, TFailure> Squash<TSuccess, TFailure>(this Result<Result<TSuccess, TFailure>, TFailure> result) => result.MapToResult(t => t);

    public static Result<TSuccess, TFailureNew> ChangeFailure<TSuccess, TFailure, TFailureNew>(this Result<TSuccess, TFailure> result, TFailureNew newValue) => result.Map(success => success, _ => newValue);

    public static Result<TSuccess, TFailureNew> ChangeFailure<TSuccess, TFailure, TFailureNew>(
        this Result<TSuccess, TFailure> result,
        Func<TFailureNew> newValue) => result.Map(success => success, _ => newValue());

    public static Result<TSuccess, TFailureNew> ChangeFailure<TSuccess, TFailure, TFailureNew>(
        this Result<TSuccess, TFailure> result,
        Func<TFailure, TFailureNew> newValue) => result.Do<Result<TSuccess, TFailureNew>>(success => success, failure => newValue(failure));

    public static T Either<T>(this Result<T, T> result) => result.Do(t => t, t => t);

    public static object? Either<TSuccess, TFailure>(this Result<TSuccess, TFailure> result) => result.Do<object?>(t => t, t => t);

    public static Result<TSuccess, TFailure> RetainIf<TSuccess, TFailure>(this Result<TSuccess, TFailure> result, Func<TSuccess, bool> predicate, TFailure replaceWith) => result.MapToResult(s => predicate(s) ? Result.Success<TSuccess, TFailure>(s) : replaceWith);

    public static Result<TSuccess, TFailure> RetainNotNull<TSuccess, TFailure>(this Result<TSuccess, TFailure> result, TFailure replaceWith)
    where TSuccess : class? => result.MapToResult(s => s is null ? replaceWith : Result.Success<TSuccess, TFailure>(s));

    public static void OnSuccess<TSuccess, TFailure>(this Result<TSuccess, TFailure> result, Action<TSuccess> action) => result.Do(action, _ => { });

    public static void OnFailure<TSuccess, TFailure>(this Result<TSuccess, TFailure> result, Action<TFailure> action) => result.Do(_ => { }, action);

    public static bool TryGetSuccess<TSuccess, TFailure>(this Result<TSuccess, TFailure> result, out TSuccess value)
    {
        if (result is Success<TSuccess, TFailure>(var success))
        {
            value = success;
            return true;
        }

        value = default!;
        return false;
    }

    public static bool TryGetFailure<TSuccess, TFailure>(this Result<TSuccess, TFailure> result, out TFailure value)
    {
        if (result is Failure<TSuccess, TFailure>(var failure))
        {
            value = failure;
            return true;
        }

        value = default!;
        return false;
    }
}
