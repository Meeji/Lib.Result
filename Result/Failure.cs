namespace Result
{
    using System;

    using CoreUtils;

    public class Failure<TSuccess, TFailure> : Result<TSuccess, TFailure>
    {
        private readonly TFailure error;

        public Failure(TFailure error)
        {
            this.error = error;
        }

        public override bool IsSuccess => false;

        public override TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure)
        {
            return ReturnParameter.OrThrowIfNull(onFailure, nameof(onFailure))(this.error);
        }
    }
}
