namespace System1Group.Core.Result
{
    using System;
    using CoreUtils;

    public class Success<TSuccess, TFailure> : Result<TSuccess, TFailure>
    {
        private readonly TSuccess item;

        public Success([System1Group.Core.Attributes.ParameterTesting.AllowedToBeNull] TSuccess item)
        {
            this.item = item;
        }

        public override bool IsSuccess => true;

        public override TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, [System1Group.Core.Attributes.ParameterTesting.AllowedToBeNull] Func<TFailure, TReturn> onFailure)
        {
            return ReturnParameter.OrThrowIfNull(onSuccess, "onSuccess")(this.item);
        }
    }
}
