namespace Result
{
    using System;
    using CoreUtils;

    public class LazyResult<TSuccess, TFailure> : Result<TSuccess, TFailure>
    {
        private readonly LazyValue<Result<TSuccess, TFailure>> lazyResult;

        public LazyResult(Func<Result<TSuccess, TFailure>> factory)
        {
            Throw.IfNull(factory, "factory");
            this.lazyResult = LazyValue.Create(factory);
        }

        public override bool IsSuccess => this.lazyResult.Value.IsSuccess;

        public override TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure)
        {
            return this.lazyResult.Value.Do(onSuccess, onFailure);
        }

        public override Result<TReturn, TFailure> Bind<TReturn>(Func<TSuccess, TReturn> bindingAction)
        {
            Throw.IfNull(bindingAction, nameof(bindingAction));
            return LazyResult.Create(() => base.Bind(bindingAction));
        }

        public override Result<TReturn, TFailure> BindToResult<TReturn>(Func<TSuccess, Result<TReturn, TFailure>> bindingAction)
        {
            Throw.IfNull(bindingAction, nameof(bindingAction));
            return LazyResult.Create(() => base.BindToResult(bindingAction));
        }

        public override Result<TReturn, TFailure> Combine<TReturn, TCombine>(
            Result<TCombine, TFailure> combineWith,
            Func<TSuccess, TCombine, TReturn> combineUsing)
        {
            Throw.IfNull(combineWith, nameof(combineWith));
            Throw.IfNull(combineUsing, nameof(combineUsing));
            return LazyResult.Create(() => base.Combine(combineWith, combineUsing));
        }

        public override Result<TReturn, TFailure> CombineToResult<TReturn, TCombine>(
            Result<TCombine, TFailure> combineWith,
            Func<TSuccess, TCombine, Result<TReturn, TFailure>> combineUsing)
        {
            Throw.IfNull(combineWith, nameof(combineWith));
            Throw.IfNull(combineUsing, nameof(combineUsing));
            return LazyResult.Create(() => base.CombineToResult(combineWith, combineUsing));
        }
    }
}
