namespace System1Group.Lib.Result
{
    using System;
    using Attributes.ParameterGeneration;
    using Attributes.ParameterTesting;
    using CoreUtils;

    public abstract partial class Result<TSuccess, TFailure> : IResult<TSuccess, TFailure>
    {
        public abstract bool IsSuccess { get; }

        public virtual bool IsFailure => !this.IsSuccess;

        public abstract TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure);

        public virtual void Do([AllowedToBeNull] Action<TSuccess> onSuccess, [AllowedToBeNull] Action<TFailure> onFailure)
        {
            Func<TSuccess, object> wrappedSuccess = null;
            Func<TFailure, object> wrappedFailure = null;

            if (onSuccess != null)
            {
                wrappedSuccess = s =>
                    {
                        onSuccess(s);
                        return null;
                    };
            }

            if (onFailure != null)
            {
                wrappedFailure = f =>
                    {
                        onFailure(f);
                        return null;
                    };
            }

            this.Do(wrappedSuccess, wrappedFailure);
        }

        public virtual TSuccess Unwrap()
        {
            return this.Unwrap(null);
        }

        public virtual TSuccess Unwrap([AllowedToBeNullEmptyOrWhitespace] string error)
        {
            return this.Do(
                item => item,
                defaultError =>
                    {
                        error = string.IsNullOrEmpty(error) ? defaultError.ToString() : error;
                        throw new InvalidOperationException(error);
                    });
        }

        public virtual TFailure UnwrapError()
        {
            return this.UnwrapError(null);
        }

        public virtual TFailure UnwrapError([AllowedToBeNullEmptyOrWhitespace] string error)
        {
            return this.Do(
                item =>
                    {
                        throw new InvalidOperationException(string.IsNullOrEmpty(error) ? "Tried to unwrap error of a Success value" : error);
                    },
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

        [Obsolete("Use Combine<Result, Function> or CombineToResult instead")]
        public virtual Result<TSuccess, TFailure> Combine<TCombine>([IsPOCO] Result<TCombine, TFailure> combineWith, Action<TSuccess, TCombine> combineUsing)
        {
            Throw.IfNull(combineWith, nameof(combineWith));
            Throw.IfNull(combineUsing, nameof(combineUsing));
            return this.BindToResult(
                t => combineWith.Bind(
                    c =>
                        {
                            combineUsing(t, c);
                            return t;
                        }));
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
