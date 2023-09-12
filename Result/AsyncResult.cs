namespace Result;

using System;
using System.Threading.Tasks;
using Exceptions;

public class AsyncResult<TSuccess, TFailure>
{
    private readonly Task<Result<TSuccess, TFailure>> task;

    public AsyncResult(Task<Result<TSuccess, TFailure>> task)
    {
        this.task = task;
    }

    public async Task<TReturn> DoAsync<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure)
    {
        var result = await this.task;
        return result.Do(onSuccess, onFailure);
    }

    public async Task DoAsync(Action<TSuccess> onSuccess, Action<TFailure> onFailure)
    {
        var result = await this.task;
        result.Do(onSuccess, onFailure);
    }

    public AsyncResult<TNew, TFailure> MapAsync<TNew>(Func<TSuccess, TNew> bindingFunc)
    {
        var result = ContinueWith(this.task, r => r.Map(bindingFunc));
        return new AsyncResult<TNew, TFailure>(result);
    }

    public virtual AsyncResult<TNewSuccess, TNewFailure> MapAsync<TNewSuccess, TNewFailure>(
        Func<TSuccess, TNewSuccess> successBindingAction,
        Func<TFailure, TNewFailure> failureBindingAction)
    {
        var result = ContinueWith(this.task, r => r.Map(successBindingAction, failureBindingAction));
        return new AsyncResult<TNewSuccess, TNewFailure>(result);
    }

    public AsyncResult<TNew, TFailure> MapToResultAsync<TNew>(Func<TSuccess, Result<TNew, TFailure>> bindingFunc)
    {
        var result = ContinueWith(this.task, r => r.MapToResult(bindingFunc));
        return new AsyncResult<TNew, TFailure>(result);
    }

    public Task<TSuccess> UnwrapAsync() => this.DoAsync(
            item => item,
            err => throw new InvalidUnwrapException(this, err, InvalidUnwrapException.UnwrapType.Success));

    public Task<TSuccess> UnwrapAsync(string error) => this.DoAsync(
            item => item,
            err => throw new InvalidUnwrapException(this, err, InvalidUnwrapException.UnwrapType.Success, error));

    public virtual Task<TFailure> UnwrapErrorAsync() => this.DoAsync(
            item => throw new InvalidUnwrapException(this, item, InvalidUnwrapException.UnwrapType.Failure),
            err => err);

    public virtual Task<TFailure> UnwrapErrorAsync(string error) => this.DoAsync(
            item => throw new InvalidUnwrapException(this, item, InvalidUnwrapException.UnwrapType.Failure, error),
            err => err);

    public virtual Task<TSuccess> OrAsync(TSuccess defaultItem) => this.DoAsync(item => item, err => defaultItem);

    public virtual Task<TSuccess> OrAsync(Func<TSuccess> defaultItemCalculator) => this.DoAsync(item => item, err => defaultItemCalculator());

    public virtual Task<TSuccess> OrAsync(Func<TFailure, TSuccess> defaultItemCalculator) => this.DoAsync(item => item, defaultItemCalculator);

    public Task<Result<TSuccess, TFailure>> ToTaskAsync() => this.task;

    private static async Task<TOut> ContinueWith<TIn, TOut>(Task<TIn> task, Func<TIn, TOut> func)
    {
        var res = await task;
        return func(res);
    }
}
