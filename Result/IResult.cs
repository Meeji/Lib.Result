namespace System1Group.Core.Result
{
    using System;

    public interface IResult<TSuccess, TFailure>
    {
        bool IsSuccess { get; }

        bool IsFailure { get; }

        TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure);

        void Do([System1Group.Core.Attributes.ParameterTesting.AllowedToBeNull] Action<TSuccess> onSuccess, [System1Group.Core.Attributes.ParameterTesting.AllowedToBeNull] Action<TFailure> onFailure);

        TSuccess Unwrap();

        TSuccess Unwrap([System1Group.Core.Attributes.ParameterTesting.AllowedToBeNullEmptyOrWhitespace] string error);

        TFailure UnwrapError();

        TFailure UnwrapError([System1Group.Core.Attributes.ParameterTesting.AllowedToBeNullEmptyOrWhitespace] string error);

        TSuccess UnwrapOr([System1Group.Core.Attributes.ParameterTesting.AllowedToBeNull, System1Group.Core.Attributes.ParameterGeneration.UseNullWhenAutomating] TSuccess defaultItem);

        TSuccess UnwrapOr(Func<TSuccess> defaultItemCalculator);

        TSuccess UnwrapOr(Func<TFailure, TSuccess> defaultItemCalculator);

        Result<TReturn, TFailure> Bind<TReturn>(Func<TSuccess, TReturn> bindingAction);

        Result<TReturn, TFailure> BindToResult<TReturn>(Func<TSuccess, Result<TReturn, TFailure>> bindingAction);

        Result<TReturn, TFailure> Combine<TReturn, TCombine>(
            [System1Group.Core.Attributes.ParameterGeneration.IsPOCO] Result<TCombine, TFailure> combineWith,
            Func<TSuccess, TCombine, TReturn> combineUsing);

        [Obsolete("Use Combine<Result, Function> or CombineToResult instead")]
        Result<TSuccess, TFailure> Combine<TCombine>([System1Group.Core.Attributes.ParameterGeneration.IsPOCO] Result<TCombine, TFailure> combineWith, Action<TSuccess, TCombine> combineUsing);

        Result<TReturn, TFailure> CombineToResult<TReturn, TCombine>(
            [System1Group.Core.Attributes.ParameterGeneration.IsPOCO] Result<TCombine, TFailure> combineWith,
            Func<TSuccess, TCombine, Result<TReturn, TFailure>> combineUsing);

        IGuardEntryPoint<TSuccess, TFailure, TOut> Guard<TOut>();
    }
}