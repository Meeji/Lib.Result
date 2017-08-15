namespace System1Group.Core.Result
{
    using System;
    using CoreUtils;

    public class Failure<TSuccess, TFailure> : Result<TSuccess, TFailure>
    {
        private readonly TFailure error;

        public Failure([System1Group.Core.Attributes.ParameterTesting.AllowedToBeNull] TFailure error)
        {
            this.error = error;
        }

        public override bool IsSuccess => false;

        public override TReturn Do<TReturn>([System1Group.Core.Attributes.ParameterTesting.AllowedToBeNull] Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure)
        {
            return ReturnParameter.OrThrowIfNull(onFailure, "onFailure")(this.error);
        }
    }
}
