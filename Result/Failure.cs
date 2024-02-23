namespace Result;

using System;

public sealed class Failure<TSuccess, TFailure> : Result<TSuccess, TFailure>, IFailure<TFailure>
{
    public TFailure Value { get; }

    public Failure(TFailure value)
    {
        this.Value = value;
    }

    public override bool IsSuccess => false;

    public override TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure) => onFailure(this.Value);

    public void Deconstruct(out TFailure failure)
    {
        failure = this.Value;
    }
}