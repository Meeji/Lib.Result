namespace System1Group.Lib.Result
{
    using Attributes.ParameterTesting;

    public static partial class Result
    {
        public static Result<TSuccess, TFailure> Failure<TSuccess, TFailure>([AllowedToBeNull]TFailure failure)
        {
            return new Failure<TSuccess, TFailure>(failure);
        }
    }
}
