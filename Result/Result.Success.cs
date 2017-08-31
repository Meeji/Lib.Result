namespace System1Group.Lib.Result
{
    using Attributes.ParameterTesting;

    public static partial class Result
    {
        public static Result<TSuccess, TFailure> Success<TSuccess, TFailure>([AllowedToBeNull]TSuccess success)
        {
            return new Success<TSuccess, TFailure>(success);
        }
    }
}
