namespace System1Group.Lib.Result
{
    public abstract partial class Result<TSuccess, TFailure>
    {
        public static implicit operator Result<TSuccess, TFailure>(TSuccess success)
        {
            return new Success<TSuccess, TFailure>(success);
        }

        public static implicit operator Result<TSuccess, TFailure>(TFailure failure)
        {
            return new Failure<TSuccess, TFailure>(failure);
        }
    }
}
