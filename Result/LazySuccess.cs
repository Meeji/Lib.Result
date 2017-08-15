namespace System1Group.Core.Result
{
    using System;
    using CoreUtils;

    public class LazySuccess<TSuccess, TFailure> : Result<TSuccess, TFailure>
    {
        private readonly LazyValue<TSuccess> lazyValue;

        public LazySuccess(Func<TSuccess> factory)
        {
            Throw.IfNull(factory, "factory");
            this.lazyValue = LazyValue.Create(factory);
        }

        public override bool IsSuccess => true;

        public override TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, [System1Group.Core.Attributes.ParameterTesting.AllowedToBeNull] Func<TFailure, TReturn> onFailure)
        {
            return ReturnParameter.OrThrowIfNull(onSuccess, "onSuccess")(this.lazyValue.Value);
        }
    }
}
