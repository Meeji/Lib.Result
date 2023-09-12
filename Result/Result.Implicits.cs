namespace Result;

public abstract partial class Result<TSuccess, TFailure>
{
    public static implicit operator Result<TSuccess, TFailure>(TSuccess success) => new Success<TSuccess, TFailure>(success);

    public static implicit operator Result<TSuccess, TFailure>(TFailure failure) => new Failure<TSuccess, TFailure>(failure);
}
