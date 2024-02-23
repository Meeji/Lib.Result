namespace Result;

using System;

public sealed class Success<TSuccess, TFailure> : Result<TSuccess, TFailure>, ISuccess<TSuccess>
{
    public TSuccess Value { get; }

    public Success(TSuccess value)
    {
        this.Value = value;
    }

    public override bool IsSuccess => true;

    public override TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure) => onSuccess(this.Value);

    public void Deconstruct(out TSuccess success)
    {
        success = this.Value;
    }
}
