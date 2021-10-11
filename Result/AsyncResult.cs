namespace System1Group.Lib.Result
{
    using System;
    using System.Threading.Tasks;
    using Attributes.ParameterTesting;
    using CoreUtils;
    using Exceptions;

    public class AsyncResult<TSuccess, TFailure> : Result<TSuccess, TFailure>
    {
        private readonly Task<Result<TSuccess, TFailure>> task;

        public AsyncResult(Task<Result<TSuccess, TFailure>> task)
        {
            Throw.IfNull(task, nameof(task));
            this.task = task;
        }

        public override bool IsSuccess
        {
            get
            {
                this.task.Wait();
                return this.task.Result.IsSuccess;
            }
        }

        public override TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure)
        {
            Throw.IfNull(onSuccess, nameof(onSuccess));
            Throw.IfNull(onFailure, nameof(onFailure));
            this.task.Wait();
            return this.task.Result.Do(onSuccess, onFailure);
        }

        public async Task<TReturn> DoAsync<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure)
        {
            Throw.IfNull(onSuccess, nameof(onSuccess));
            Throw.IfNull(onFailure, nameof(onFailure));
            var result = await this.task;
            return result.Do(onSuccess, onFailure);
        }

        public async Task DoAsync(Action<TSuccess> onSuccess, Action<TFailure> onFailure)
        {
            Throw.IfNull(onSuccess, nameof(onSuccess));
            Throw.IfNull(onFailure, nameof(onFailure));
            var result = await this.task;
            result.Do(onSuccess, onFailure);
        }

        public AsyncResult<TNew, TFailure> BindAsync<TNew>(Func<TSuccess, TNew> bindingFunc)
        {
            Throw.IfNull(bindingFunc, nameof(bindingFunc));
            var result = SafeContinueWith(this.task, r => r.Bind(bindingFunc));
            return new AsyncResult<TNew, TFailure>(result);
        }

        public virtual AsyncResult<TNewSuccess, TNewFailure> BindAsync<TNewSuccess, TNewFailure>(
            Func<TSuccess, TNewSuccess> successBindingAction,
            Func<TFailure, TNewFailure> failureBindingAction)
        {
            Throw.IfNull(successBindingAction, nameof(successBindingAction));
            Throw.IfNull(failureBindingAction, nameof(failureBindingAction));
            var result = SafeContinueWith(this.task, r => r.Bind(successBindingAction, failureBindingAction));
            return new AsyncResult<TNewSuccess, TNewFailure>(result);
        }

        public AsyncResult<TNew, TFailure> BindToResultAsync<TNew>(Func<TSuccess, Result<TNew, TFailure>> bindingFunc)
        {
            Throw.IfNull(bindingFunc, nameof(bindingFunc));
            var result = SafeContinueWith(this.task, r => r.BindToResult(bindingFunc));
            return new AsyncResult<TNew, TFailure>(result);
        }

        public Task<TSuccess> UnwrapAsync([AllowedToBeNullEmptyOrWhitespace] string error = null)
        {
            return this.DoAsync(
                item => item,
                err => throw new InvalidUnwrapException(this, err, InvalidUnwrapException.UnwrapType.Success, error));
        }

        public virtual Task<TFailure> UnwrapErrorAsync([AllowedToBeNullEmptyOrWhitespace] string error = null)
        {
            return this.DoAsync(
                item => throw new InvalidUnwrapException(this, item, InvalidUnwrapException.UnwrapType.Failure, error),
                err => err);
        }

        public virtual Task<TSuccess> UnwrapOrAsync([AllowedToBeNull] TSuccess defaultItem)
        {
            return this.DoAsync(item => item, err => defaultItem);
        }

        public virtual Task<TSuccess> UnwrapOrAsync(Func<TSuccess> defaultItemCalculator)
        {
            Throw.IfNull(defaultItemCalculator, nameof(defaultItemCalculator));
            return this.DoAsync(item => item, err => defaultItemCalculator());
        }

        public virtual Task<TSuccess> UnwrapOrAsync(Func<TFailure, TSuccess> defaultItemCalculator)
        {
            Throw.IfNull(defaultItemCalculator, nameof(defaultItemCalculator));
            return this.DoAsync(item => item, defaultItemCalculator);
        }

        public Task<Result<TSuccess, TFailure>> ToTask()
        {
            return this.task;
        }

        private static async Task<TOut> SafeContinueWith<TIn, TOut>(Task<TIn> task, Func<TIn, TOut> func)
        {
            var res = await task;
            return func(res);

            // TODO: this is the previous implementation
            // TODO: To run it in unit tests you need to add extra steps to set up a SynchronizationContext.
            // TODO: There may be other edge case situations where this implemtation has issues... but it will be quicker.
            // TODO: Might be worth investigating at some point how safe it is, how fast it is, and maybe re-introduce it.
            /*
            return task.ContinueWith(
                t =>
                {
                    if (t.Exception != null)
                    {
                        throw t.Exception.InnerException ?? t.Exception;
                    }

                    return func(t.Result);
                },
                TaskScheduler.FromCurrentSynchronizationContext());
           */
        }
    }
}
