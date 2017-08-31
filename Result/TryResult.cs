namespace System1Group.Lib.Result
{
    using System;
    using Attributes.ParameterTesting;
    using CoreUtils;

    public class TryResult<TSuccess> : Result<TSuccess, Exception>
    {
        private readonly LazyValue<Result<TSuccess, Exception>> lazyValue;

        public TryResult(Func<TSuccess> factory)
        {
            Throw.IfNull(factory, "factory");
            this.lazyValue = LazyValue.Create(() => TryRunFactory(factory));
        }

        public override bool IsSuccess => this.lazyValue.Value.IsSuccess;

        public override TReturn Do<TReturn>([AllowedToBeNull] Func<TSuccess, TReturn> onSuccess, [AllowedToBeNull] Func<Exception, TReturn> onFailure)
        {
            return this.lazyValue.Value.Do(onSuccess, onFailure);
        }

        private static Result<TSuccess, Exception> TryRunFactory(Func<TSuccess> factory)
        {
            try
            {
                return new Success<TSuccess, Exception>(factory());
            }
            catch (Exception exc)
            {
                return new Failure<TSuccess, Exception>(exc);
            }
        }
    }
}
