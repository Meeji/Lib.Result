﻿namespace System1Group.Lib.Result
{
    using System;
    using Attributes.ParameterTesting;
    using CoreUtils;

    public class LazyFailure<TSuccess, TFailure> : Result<TSuccess, TFailure>
    {
        private readonly LazyValue<TFailure> lazyError;

        public LazyFailure(Func<TFailure> factory)
        {
            Throw.IfNull(factory, "factory");
            this.lazyError = LazyValue.Create(factory);
        }

        public override bool IsSuccess => false;

        public override TReturn Do<TReturn>([AllowedToBeNull] Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure)
        {
            return ReturnParameter.OrThrowIfNull(onFailure, "onFailure")(this.lazyError.Value);
        }
    }
}
