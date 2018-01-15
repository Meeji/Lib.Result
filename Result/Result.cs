namespace System1Group.Lib.Result
{
    using System;
    using Attributes.ParameterGeneration;
    using Attributes.ParameterTesting;
    using CoreUtils;
    using Exceptions;

    public abstract partial class Result<TSuccess, TFailure>
    {
        public abstract bool IsSuccess { get; }

        public virtual bool IsFailure => !this.IsSuccess;

        public abstract TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure);

        public virtual void Do(Action<TSuccess> onSuccess, Action<TFailure> onFailure)
        {
            Throw.IfNull(onSuccess, nameof(onSuccess));
            Throw.IfNull(onFailure, nameof(onFailure));

            this.Do<object>(
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
        }

        public virtual TSuccess Unwrap([AllowedToBeNullEmptyOrWhitespace] string error = null)
        {
            return this.Do(
                item => item,
                err => throw new InvalidUnwrapException(this, err, InvalidUnwrapException.UnwrapType.Success, error));
        }

        public virtual TFailure UnwrapError([AllowedToBeNullEmptyOrWhitespace] string error = null)
        {
            return this.Do(
                item => throw new InvalidUnwrapException(this, item, InvalidUnwrapException.UnwrapType.Failure, error),
                err => err);
        }

        public virtual TSuccess UnwrapOr([AllowedToBeNull] TSuccess defaultItem)
        {
            return this.Do(item => item, err => defaultItem);
        }

        public virtual TSuccess UnwrapOr(Func<TSuccess> defaultItemCalculator)
        {
            Throw.IfNull(defaultItemCalculator, nameof(defaultItemCalculator));
            return this.Do(item => item, err => defaultItemCalculator());
        }

        public virtual TSuccess UnwrapOr(Func<TFailure, TSuccess> defaultItemCalculator)
        {
            Throw.IfNull(defaultItemCalculator, nameof(defaultItemCalculator));
            return this.Do(item => item, defaultItemCalculator);
        }

        public virtual Result<TReturn, TFailure> Bind<TReturn>(Func<TSuccess, TReturn> bindingAction)
        {
            Throw.IfNull(bindingAction, nameof(bindingAction));
            return this.Do<Result<TReturn, TFailure>>(item => bindingAction(item), err => err);
        }

        public virtual Result<TNewSuccess, TNewFailure> Bind<TNewSuccess, TNewFailure>(
            Func<TSuccess, TNewSuccess> successBindingAction,
            Func<TFailure, TNewFailure> failureBindingAction)
        {
            Throw.IfNull(successBindingAction, nameof(successBindingAction));
            Throw.IfNull(failureBindingAction, nameof(failureBindingAction));
            return this.Do<Result<TNewSuccess, TNewFailure>>(item => successBindingAction(item), err => failureBindingAction(err));
        }

        public virtual Result<TReturn, TFailure> BindToResult<TReturn>(Func<TSuccess, Result<TReturn, TFailure>> bindingAction)
        {
            Throw.IfNull(bindingAction, nameof(bindingAction));
            return this.Do(bindingAction, err => err);
        }

        public virtual Result<TReturn, TFailure> Combine<TReturn, TCombine>(
            [IsPOCO] Result<TCombine, TFailure> combineWith,
            Func<TSuccess, TCombine, TReturn> combineUsing)
        {
            Throw.IfNull(combineWith, nameof(combineWith));
            Throw.IfNull(combineUsing, nameof(combineUsing));
            return this.BindToResult(t => combineWith.Bind(c => combineUsing(t, c)));
        }

        public virtual Result<TReturn, TFailure> CombineToResult<TReturn, TCombine>(
            [IsPOCO] Result<TCombine, TFailure> combineWith,
            Func<TSuccess, TCombine, Result<TReturn, TFailure>> combineUsing)
        {
            Throw.IfNull(combineWith, nameof(combineWith));
            Throw.IfNull(combineUsing, nameof(combineUsing));
            return this.BindToResult(t => combineWith.BindToResult(c => combineUsing(t, c)));
        }

        public virtual IGuardEntryPoint<TSuccess, TFailure, TOut> Guard<TOut>()
        {
            return new ResultGuard<TSuccess, TFailure, TOut>(this); // Not very testable, but Result can't really be provided by IOC so it can't be
        }
    }
}
