namespace Result.Unsafe;

using Exceptions;

public static class ResultExtensionMethods
{
    public static TSuccess Unwrap<TSuccess, TFailure>(this Result<TSuccess, TFailure> result) => result.Do(
        item => item,
        err => throw new InvalidUnwrapException(result, err, InvalidUnwrapException.UnwrapType.Success));

    public static TSuccess Unwrap<TSuccess, TFailure>(this Result<TSuccess, TFailure> result, string error) => result.Do(
        item => item,
        err => throw new InvalidUnwrapException(result, err, InvalidUnwrapException.UnwrapType.Success, error));

    public static TFailure UnwrapError<TSuccess, TFailure>(this Result<TSuccess, TFailure> result) => result.Do(
        item => throw new InvalidUnwrapException(result, item, InvalidUnwrapException.UnwrapType.Failure),
        err => err);

    public static TFailure UnwrapError<TSuccess, TFailure>(this Result<TSuccess, TFailure> result, string error) => result.Do(
        item => throw new InvalidUnwrapException(result, item, InvalidUnwrapException.UnwrapType.Failure, error),
        err => err);
    
    public static TSuccess OrThrow<TSuccess, TFailure>(this Result<TSuccess, TFailure> result)
        where TFailure : Exception =>
        result.Do(
            success => success,
            failure => throw failure);
    
    public static Task<TSuccess> OrThrowAsync<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> result)
        where TFailure : Exception =>
        result.DoAsync(
            success => success,
            failure => throw failure);
}