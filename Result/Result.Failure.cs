namespace System1Group.Lib.Result
{
    public static partial class Result
    {
        public static Result<TSuccess, TFailure> Failure<TSuccess, TFailure>([System1Group.Lib.Attributes.ParameterTesting.AllowedToBeNull]TFailure failure)
        {
            return new Failure<TSuccess, TFailure>(failure);
        }
    }
}
