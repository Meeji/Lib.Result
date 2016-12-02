namespace Result
{
    using System;

    public interface IResult<TSuccess, TFailure>
    {
        bool IsSuccess { get; }

        bool IsFailure { get; }

        TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure);

        void Do(Action<TSuccess> onSuccess, Action<TFailure> onFailure);

        TSuccess Unwrap();

        TSuccess Unwrap(string error);

        TFailure UnwrapError();

        TFailure UnwrapError(string error);

        TSuccess UnwrapOr(TSuccess defaultItem);

        TSuccess UnwrapOr(Func<TSuccess> defaultItemCalculator);

        TSuccess UnwrapOr(Func<TFailure, TSuccess> defaultItemCalculator);

        Result<TReturn, TFailure> Bind<TReturn>(Func<TSuccess, TReturn> bindingAction);

        Result<TReturn, TFailure> BindToResult<TReturn>(Func<TSuccess, Result<TReturn, TFailure>> bindingAction);

        Result<TReturn, TFailure> Combine<TReturn, TCombine>(Result<TCombine, TFailure> combineWith, Func<TSuccess, TCombine, TReturn> combineUsing);

        Result<TReturn, TFailure> CombineToResult<TReturn, TCombine>(
            Result<TCombine, TFailure> combineWith,
            Func<TSuccess, TCombine, Result<TReturn, TFailure>> combineUsing);

        IGuardEntryPoint<TSuccess, TFailure, TOut> Guard<TOut>();
    }
}