namespace System1Group.Lib.Result
{
    using Attributes.ParameterTesting;

    public static class ErrorResult
    {
        public static ErrorResult<TFailure> NoError<TFailure>() => new ErrorResult<TFailure>();

        public static ErrorResult<TFailure> WithError<TFailure>([AllowedToBeNull] TFailure failure) => new ErrorResult<TFailure>(failure);
    }
}