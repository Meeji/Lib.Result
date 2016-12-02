namespace Result
{
    using System;

    public interface IGuardEntryPoint<TSuccess, TFailure, TOut>
    {
        IGuardSuccessOpen<TSuccess, TFailure, TOut> Success();

        IGuardFailureOpen<TSuccess, TFailure, TOut> Failure();
    }

    public interface IGuardAllClosing<TOut>
    {
        TOut Do();
    }

    public interface IGuardSuccess<TSuccess, TFailure, TOut>
    {
        IGuardSuccess<TSuccess, TFailure, TOut> Where(Func<TSuccess, bool> checker, Func<TSuccess, TOut> result);

        IGuardAllClosing<TOut> Default(Func<TSuccess, TOut> result);
    }

    public interface IGuardSuccessOpen<TSuccess, TFailure, TOut>
    {
        IGuardSuccessOpen<TSuccess, TFailure, TOut> Where(Func<TSuccess, bool> checker, Func<TSuccess, TOut> result);

        IGuardSuccessClosing<TSuccess, TFailure, TOut> Default(Func<TSuccess, TOut> result);
    }

    public interface IGuardSuccessClosing<TSuccess, TFailure, TOut>
    {
        IGuardFailure<TSuccess, TFailure, TOut> Failure();
    }

    public interface IGuardFailure<TSuccess, TFailure, TOut>
    {
        IGuardFailure<TSuccess, TFailure, TOut> Where(Func<TFailure, bool> checker, Func<TFailure, TOut> result);

        IGuardAllClosing<TOut> Default(Func<TFailure, TOut> result);
    }

    public interface IGuardFailureOpen<TSuccess, TFailure, TOut>
    {
        IGuardFailureOpen<TSuccess, TFailure, TOut> Where(Func<TFailure, bool> checker, Func<TFailure, TOut> result);

        IGuardFailureClosing<TSuccess, TFailure, TOut> Default(Func<TFailure, TOut> result);
    }

    public interface IGuardFailureClosing<TSuccess, TFailure, TOut>
    {
        IGuardSuccess<TSuccess, TFailure, TOut> Success();
    }
}