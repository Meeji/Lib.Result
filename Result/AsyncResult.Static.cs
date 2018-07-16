namespace System1Group.Lib.Result
{
    using System;
    using System.Threading.Tasks;

    public static class AsyncResult
    {
        public static AsyncResult<TSuccess, TFailure> Create<TSuccess, TFailure>(Result<TSuccess, TFailure> result)
        {
            return new AsyncResult<TSuccess, TFailure>(Task.FromResult(result));
        }

        public static AsyncResult<TSuccess, TFailure> Create<TSuccess, TFailure>(Task<Result<TSuccess, TFailure>> task)
        {
            return new AsyncResult<TSuccess, TFailure>(task);
        }

        public static AsyncResult<TSuccess, TFailure> Create<TSuccess, TFailure>(Func<Result<TSuccess, TFailure>> func)
        {
            return new AsyncResult<TSuccess, TFailure>(Task.Run(func));
        }

        public static AsyncResult<TSuccess, TFailure> ToAsyncResult<TSuccess, TFailure>(this Func<Result<TSuccess, TFailure>> func)
        {
            return Create(func);
        }

        public static AsyncResult<TSuccess, TFailure> ToAsyncResult<TSuccess, TFailure>(this Task<Result<TSuccess, TFailure>> task)
        {
            return Create(task);
        }

        public static AsyncResult<TSuccess, TFailure> ToAsyncResult<TSuccess, TFailure>(this Result<TSuccess, TFailure> result)
        {
            return Create(Task.FromResult(result));
        }
    }
}
