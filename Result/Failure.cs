namespace Result;

using System;

public sealed class Failure<TSuccess, TFailure> : Result<TSuccess, TFailure>
{
    private readonly TFailure error;

    public Failure(TFailure error)
    {
        this.error = error;
    }

    public override bool IsSuccess => false;

    public override TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure) => onFailure(this.error);

    public void Deconstruct(out TFailure failure)
    {
        failure = this.error;
    }
}