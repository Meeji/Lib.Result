namespace System1Group.Core.Result
{
    public static partial class Result
    {
        public static Result<TSuccess, TFailure> Failure<TSuccess, TFailure>([System1Group.Core.Attributes.ParameterTesting.AllowedToBeNull]TFailure failure)
        {
            return new Failure<TSuccess, TFailure>(failure);
        }
    }
}
