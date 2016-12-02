namespace Result
{
    using System;

    public static class LazyResult
    {
        public static LazyResult<TSuccess, TFailure> Create<TSuccess, TFailure>(Func<Result<TSuccess, TFailure>> factory)
        {
            return new LazyResult<TSuccess, TFailure>(factory);
        }
    }
}
