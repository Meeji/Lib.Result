namespace Result;

using System;
using System.Threading.Tasks;

public static class TaskExtensionMethods
{
    public static Task<Result<TNew, TFailure>> MapAsync<TSuccess, TFailure, TNew>(
        this Result<TSuccess, TFailure> result,
        Func<TSuccess, Task<TNew>> bindingFunc) => result.Do(
            async success => Result.Success<TNew, TFailure>(await bindingFunc(success)),
            failure => Task.FromResult(Result.Failure<TNew, TFailure>(failure)));

    public static async Task<Result<TNew, TFailure>> MapAsync<TSuccess, TFailure, TNew>(
        this Task<Result<TSuccess, TFailure>> task,
        Func<TSuccess, Task<TNew>> bindingFunc)
    {
        var result = await task;
        return await result.Do(
            async success => Result.Success<TNew, TFailure>(await bindingFunc(success)),
            failure => Task.FromResult(Result.Failure<TNew, TFailure>(failure)));
    }

    public static Task<Result<TNew, TFailure>> MapToResultAsync<TSuccess, TFailure, TNew>(
        this Result<TSuccess, TFailure> result,
        Func<TSuccess, Task<Result<TNew, TFailure>>> bindingFunc) => result.Do(
            bindingFunc,
            failure => Task.FromResult(Result.Failure<TNew, TFailure>(failure)));

    public static async Task<Result<TNew, TFailure>> MapToResultAsync<TSuccess, TFailure, TNew>(
        this Task<Result<TSuccess, TFailure>> task,
        Func<TSuccess, Task<Result<TNew, TFailure>>> bindingFunc)
    {
        var result = await task;
        return await result.Do(
                    bindingFunc,
                    failure => Task.FromResult(Result.Failure<TNew, TFailure>(failure)));
    }
}
