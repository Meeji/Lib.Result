namespace System1Group.Core.Result
{
    public static partial class Result
    {
        public static Result<TSuccess, TFailure> Success<TSuccess, TFailure>([System1Group.Core.Attributes.ParameterTesting.AllowedToBeNull]TSuccess success)
        {
            return new Success<TSuccess, TFailure>(success);
        }
    }
}
