namespace Result;

using System;

public class Success<TSuccess, TFailure> : Result<TSuccess, TFailure>
{
    private readonly TSuccess item;

    public Success(TSuccess item)
    {
        this.item = item;
    }

    public override bool IsSuccess => true;

    public override TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure) => onSuccess(this.item);

    public void Deconstruct(out TSuccess success)
    {
        success = this.item;
    }
}
