namespace System1Group.Lib.Result
{
    using System;
    using Attributes.ParameterTesting;
    using CoreUtils;

    /// <summary>
    /// Specialised Result`TS`TF type for cases where there is no Success value and we only care about whether an error has occurred. Assignable to Result`bool`TF
    /// </summary>
    /// <typeparam name="TFailure">value of error if an error occurred</typeparam>
    public class ErrorResult<TFailure> : Result<bool, TFailure>
    {
        private readonly TFailure failure;

        private ErrorResult()
        {
            this.IsSuccess = true;
        }

        private ErrorResult([AllowedToBeNull] TFailure failure)
        {
            this.IsSuccess = false;
            this.failure = failure;
        }

        public override bool IsSuccess { get; }

        public static implicit operator ErrorResult<TFailure>([AllowedToBeNull] TFailure error) => WithError(error);

        public static ErrorResult<TFailure> NoError() => new ErrorResult<TFailure>();

        public static ErrorResult<TFailure> WithError([AllowedToBeNull] TFailure failure) => new ErrorResult<TFailure>(failure);

        public override TReturn Do<TReturn>(Func<bool, TReturn> onSuccess, Func<TFailure, TReturn> onFailure)
        {
            Throw.IfNull(onSuccess, nameof(onSuccess));
            Throw.IfNull(onFailure, nameof(onFailure));

            return this.IsSuccess ? onSuccess(true) : onFailure(this.failure);
        }
    }
}