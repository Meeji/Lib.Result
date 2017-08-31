namespace System1Group.Lib.Result
{
    using System;
    using Attributes.ParameterGeneration;
    using Attributes.ParameterTesting;

    public interface IResult<TSuccess, TFailure>
    {
        bool IsSuccess { get; }

        bool IsFailure { get; }

        TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure);

        void Do([AllowedToBeNull] Action<TSuccess> onSuccess, [AllowedToBeNull] Action<TFailure> onFailure);

        TSuccess Unwrap();

        TSuccess Unwrap([AllowedToBeNullEmptyOrWhitespace] string error);

        TFailure UnwrapError();

        TFailure UnwrapError([AllowedToBeNullEmptyOrWhitespace] string error);

        TSuccess UnwrapOr([AllowedToBeNull] TSuccess defaultItem);

        TSuccess UnwrapOr(Func<TSuccess> defaultItemCalculator);

        TSuccess UnwrapOr(Func<TFailure, TSuccess> defaultItemCalculator);

        Result<TReturn, TFailure> Bind<TReturn>(Func<TSuccess, TReturn> bindingAction);

        Result<TReturn, TFailure> BindToResult<TReturn>(Func<TSuccess, Result<TReturn, TFailure>> bindingAction);

        Result<TReturn, TFailure> Combine<TReturn, TCombine>(
            [IsPOCO] Result<TCombine, TFailure> combineWith,
            Func<TSuccess, TCombine, TReturn> combineUsing);

        [Obsolete("Use Combine<Result, Function> or CombineToResult instead")]
        Result<TSuccess, TFailure> Combine<TCombine>([IsPOCO] Result<TCombine, TFailure> combineWith, Action<TSuccess, TCombine> combineUsing);

        Result<TReturn, TFailure> CombineToResult<TReturn, TCombine>(
            [IsPOCO] Result<TCombine, TFailure> combineWith,
            Func<TSuccess, TCombine, Result<TReturn, TFailure>> combineUsing);

        IGuardEntryPoint<TSuccess, TFailure, TOut> Guard<TOut>();
    }
}