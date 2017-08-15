namespace System1Group.Core.Result
{
    using System;

    public abstract partial class Result<TSuccess, TFailure>
    {
        public static implicit operator Result<TSuccess, TFailure>(TSuccess success)
        {
            return new Success<TSuccess, TFailure>(success);
        }

        public static implicit operator Result<TSuccess, TFailure>(TFailure failure)
        {
            return new Failure<TSuccess, TFailure>(failure);
        }

        public static implicit operator Result<TSuccess, TFailure>(Func<TSuccess> successFunc)
        {
            return new LazySuccess<TSuccess, TFailure>(successFunc);
        }

        public static implicit operator Result<TSuccess, TFailure>(Func<TFailure> failureFunc)
        {
            return new LazyFailure<TSuccess, TFailure>(failureFunc);
        }
    }
}
