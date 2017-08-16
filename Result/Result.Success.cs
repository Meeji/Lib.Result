namespace System1Group.Lib.Result
{
    public static partial class Result
    {
        public static Result<TSuccess, TFailure> Success<TSuccess, TFailure>([System1Group.Lib.Attributes.ParameterTesting.AllowedToBeNull]TSuccess success)
        {
            return new Success<TSuccess, TFailure>(success);
        }
    }
}
