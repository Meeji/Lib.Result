namespace System1Group.Core.Result.ResultOptionInteropExtensions
{
    using System;
    using CoreUtils;
    using Optional;

    public static class ResultOptionInteropExtensions
    {
        public static Option<TS> UnwrapOption<TS, TF>([System1Group.Core.Attributes.ParameterGeneration.DoesNotIntroduceCoupling]this Result<Option<TS>, TF> result)
        {
            Throw.IfNull(result, nameof(result));

            return result.UnwrapOr(Option.None<TS>);
        }

        public static Option<TS> ToOption<TS, TF>([System1Group.Core.Attributes.ParameterGeneration.DoesNotIntroduceCoupling]this Result<TS, TF> result)
        {
            Throw.IfNull(result, nameof(result));

            return result.Do(Option.Some, _ => Option.None<TS>());
        }

        public static Result<TS, TF> ToResult<TS, TF>(this Option<TS> option, [System1Group.Core.Attributes.ParameterTesting.AllowedToBeNull] TF failure)
        {
            Throw.IfNull(option, nameof(option));

            return option.Match(Result.Success<TS, TF>, () => failure);
        }

        public static Result<TS, TF> ToResult<TS, TF>(this Option<TS> option, Func<TF> failure)
        {
            Throw.IfNull(option, nameof(option));
            Throw.IfNull(failure, nameof(failure));

            return option.Match(Result.Success<TS, TF>, () => failure());
        }

        public static TR Match<TS, TF, TR>([System1Group.Core.Attributes.ParameterGeneration.DoesNotIntroduceCoupling]this Result<Option<TS>, TF> result, Func<TS, TR> some, Func<TR> none)
        {
            Throw.IfNull(result, nameof(result));
            Throw.IfNull(some, nameof(some));
            Throw.IfNull(none, nameof(none));

            return result.Do(success => success.Match(some, none), _ => none());
        }

        public static TR Do<TS, TF, TR>(this Option<Result<TS, TF>> option, Func<TS, TR> onSuccess, Func<TF, TR> onFailure, [System1Group.Core.Attributes.ParameterTesting.AllowedToBeNull] TF assumedFailure)
        {
            Throw.IfNull(option, nameof(option));
            Throw.IfNull(onSuccess, nameof(onSuccess));
            Throw.IfNull(onFailure, nameof(onFailure));

            return option.Match(result => result.Do(onSuccess, onFailure), () => onFailure(assumedFailure));
        }
    }
}
