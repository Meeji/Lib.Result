using Result.Exceptions;

namespace Result.Unsafe;

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
}