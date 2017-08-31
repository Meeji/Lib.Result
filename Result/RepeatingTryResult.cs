namespace System1Group.Lib.Result
{
    using System;
    using Attributes.ParameterTesting;
    using CoreUtils;

    public class RepeatingTryResult<TSuccess> : Result<TSuccess, Exception>
    {
        private readonly Func<TSuccess> factory;

        public RepeatingTryResult(Func<TSuccess> factory)
        {
            this.factory = ReturnParameter.OrThrowIfNull(factory, "factory");
        }

        public override bool IsSuccess => this.TryRunFactory().IsSuccess;

        public override TReturn Do<TReturn>([AllowedToBeNull] Func<TSuccess, TReturn> onSuccess, [AllowedToBeNull] Func<Exception, TReturn> onFailure)
        {
            return this.TryRunFactory().Do(onSuccess, onFailure);
        }

        private Result<TSuccess, Exception> TryRunFactory()
        {
            try
            {
                return this.factory();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
