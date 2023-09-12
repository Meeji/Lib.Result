namespace Result;

using System;
using System.Threading.Tasks;

public static class AsyncResult
{
    public static AsyncResult<TSuccess, TFailure> Create<TSuccess, TFailure>(Result<TSuccess, TFailure> result) => new(Task.FromResult(result));

    public static AsyncResult<TSuccess, TFailure> Create<TSuccess, TFailure>(Task<Result<TSuccess, TFailure>> task) => new(task);

    public static AsyncResult<TSuccess, TFailure> Create<TSuccess, TFailure>(Func<Result<TSuccess, TFailure>> func) => new(Task.Run(func));

    public static AsyncResult<TSuccess, TFailure> ToAsyncResult<TSuccess, TFailure>(this Func<Result<TSuccess, TFailure>> func) => Create(func);

    public static AsyncResult<TSuccess, TFailure> ToAsyncResult<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> task) => Create(task);

    public static AsyncResult<TSuccess, TFailure> ToAsyncResult<TSuccess, TFailure>(this Result<TSuccess, TFailure> result) => Create(Task.FromResult(result));
}
