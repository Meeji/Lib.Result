namespace System1Group.Lib.Result.ResultOptionInteropExtensions
{
    using System;
    using Attributes.ParameterGeneration;
    using Attributes.ParameterTesting;
    using CoreUtils;
    using Optional;

    public static class ResultOptionInteropExtensions
    {
        public static Option<TS> UnwrapOption<TS, TF>([DoesNotIntroduceCoupling]this Result<Option<TS>, TF> result)
        {
            Throw.IfNull(result, nameof(result));

            return result.UnwrapOr(Option.None<TS>);
        }

        public static Option<TS> ToOption<TS, TF>([DoesNotIntroduceCoupling]this Result<TS, TF> result)
        {
            Throw.IfNull(result, nameof(result));

            return result.Do(Option.Some, _ => Option.None<TS>());
        }

        public static Result<TS, TF> ToResult<TS, TF>(this Option<TS> option, [AllowedToBeNull] TF failure)
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

        public static TR Match<TS, TF, TR>([DoesNotIntroduceCoupling]this Result<Option<TS>, TF> result, Func<TS, TR> some, Func<TR> none)
        {
            Throw.IfNull(result, nameof(result));
            Throw.IfNull(some, nameof(some));
            Throw.IfNull(none, nameof(none));

            return result.Do(success => success.Match(some, none), _ => none());
        }

        public static TR Do<TS, TF, TR>(this Option<Result<TS, TF>> option, Func<TS, TR> onSuccess, Func<TF, TR> onFailure, [AllowedToBeNull] TF assumedFailure)
        {
            Throw.IfNull(option, nameof(option));
            Throw.IfNull(onSuccess, nameof(onSuccess));
            Throw.IfNull(onFailure, nameof(onFailure));

            return option.Match(result => result.Do(onSuccess, onFailure), () => onFailure(assumedFailure));
        }
    }
}
