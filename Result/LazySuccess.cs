namespace Result
{
    using System;

    using CoreUtils;

    public class LazySuccess<TSuccess, TFailure> : Result<TSuccess, TFailure>
    {
        private readonly LazyValue<TSuccess> lazyValue;

        public LazySuccess(Func<TSuccess> factory)
        {
            Throw.IfNull(factory, nameof(factory));
            this.lazyValue = LazyValue.Create(factory);
        }

        public override bool IsSuccess => true;

        public override TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure)
        {
            return ReturnParameter.OrThrowIfNull(onSuccess, nameof(onSuccess))(this.lazyValue.Value);
        }
    }
}
