namespace Result;

using System;
using Exceptions;

public abstract partial class Result<TSuccess, TFailure>
{
    public abstract bool IsSuccess { get; }

    public virtual bool IsFailure => !this.IsSuccess;

    public abstract TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure);

    public virtual void Do(Action<TSuccess> onSuccess, Action<TFailure> onFailure) => this.Do<object?>(
            item =>
            {
                onSuccess(item);
                return null;
            },
            err =>
            {
                onFailure(err);
                return null;
            });

    public virtual TSuccess Or(TSuccess defaultItem) => this.Do(item => item, err => defaultItem);

    public virtual TSuccess Or(Func<TSuccess> defaultItemCalculator) => this.Do(item => item, err => defaultItemCalculator());

    public virtual TSuccess Or(Func<TFailure, TSuccess> defaultItemCalculator) => this.Do(item => item, defaultItemCalculator);

    public virtual Result<TReturn, TFailure> Then<TReturn>(Func<TSuccess, TReturn> bindingAction) => this.Do<Result<TReturn, TFailure>>(item => bindingAction(item), err => err);

    public virtual Result<TNewSuccess, TNewFailure> Then<TNewSuccess, TNewFailure>(
        Func<TSuccess, TNewSuccess> successBindingAction,
        Func<TFailure, TNewFailure> failureBindingAction) => this.Do<Result<TNewSuccess, TNewFailure>>(item => successBindingAction(item), err => failureBindingAction(err));

    public virtual Result<TReturn, TFailure> Then<TReturn>(Func<TSuccess, Result<TReturn, TFailure>> bindingAction) => this.Do(bindingAction, err => err);
}
