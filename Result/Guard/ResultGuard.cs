namespace System1Group.Lib.Result
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CoreUtils;

    public sealed class ResultGuard<TSuccess, TFailure, TOut> : IGuardEntryPoint<TSuccess, TFailure, TOut>,
                                                         IGuardAllClosing<TOut>,
                                                         IGuardSuccess<TSuccess, TFailure, TOut>,
                                                         IGuardSuccessOpen<TSuccess, TFailure, TOut>,
                                                         IGuardSuccessClosing<TSuccess, TFailure, TOut>,
                                                         IGuardFailure<TSuccess, TFailure, TOut>,
                                                         IGuardFailureOpen<TSuccess, TFailure, TOut>,
                                                         IGuardFailureClosing<TSuccess, TFailure, TOut>
    {
        private readonly Result<TSuccess, TFailure> innerResult;

        private readonly List<CheckAndCall<TSuccess>> successCalls = new List<CheckAndCall<TSuccess>>();

        private readonly List<CheckAndCall<TFailure>> failureCalls = new List<CheckAndCall<TFailure>>();

        private Func<TSuccess, TOut> defaultSuccessCall;

        private Func<TFailure, TOut> defaultFailureCall;

        public ResultGuard(Result<TSuccess, TFailure> innerResult)
        {
            this.innerResult = ReturnParameter.OrThrowIfNull(innerResult, nameof(innerResult));
        }

        IGuardSuccessClosing<TSuccess, TFailure, TOut> IGuardSuccessOpen<TSuccess, TFailure, TOut>.Default(Func<TSuccess, TOut> call)
        {
            Throw.IfNull(call, nameof(call));
            this.defaultSuccessCall = call;
            return this;
        }

        public IGuardAllClosing<TOut> Default(Func<TSuccess, TOut> call)
        {
            Throw.IfNull(call, nameof(call));
            this.defaultSuccessCall = call;
            return this;
        }

        IGuardFailureClosing<TSuccess, TFailure, TOut> IGuardFailureOpen<TSuccess, TFailure, TOut>.Default(Func<TFailure, TOut> call)
        {
            Throw.IfNull(call, nameof(call));
            this.defaultFailureCall = call;
            return this;
        }

        public IGuardAllClosing<TOut> Default(Func<TFailure, TOut> call)
        {
            Throw.IfNull(call, nameof(call));
            this.defaultFailureCall = call;
            return this;
        }

        public IGuardSuccess<TSuccess, TFailure, TOut> Where(Func<TSuccess, bool> check, Func<TSuccess, TOut> call)
        {
            Throw.IfNull(check, nameof(check));
            Throw.IfNull(call, nameof(call));
            this.successCalls.Add(new CheckAndCall<TSuccess>(check, call));
            return this;
        }

        IGuardSuccessOpen<TSuccess, TFailure, TOut> IGuardSuccessOpen<TSuccess, TFailure, TOut>.Where(Func<TSuccess, bool> check, Func<TSuccess, TOut> call)
        {
            Throw.IfNull(check, nameof(check));
            Throw.IfNull(call, nameof(call));
            this.successCalls.Add(new CheckAndCall<TSuccess>(check, call));
            return this;
        }

        IGuardFailure<TSuccess, TFailure, TOut> IGuardFailure<TSuccess, TFailure, TOut>.Where(Func<TFailure, bool> check, Func<TFailure, TOut> call)
        {
            Throw.IfNull(check, nameof(check));
            Throw.IfNull(call, nameof(call));
            this.failureCalls.Add(new CheckAndCall<TFailure>(check, call));
            return this;
        }

        public IGuardFailureOpen<TSuccess, TFailure, TOut> Where(Func<TFailure, bool> check, Func<TFailure, TOut> call)
        {
            Throw.IfNull(check, nameof(check));
            Throw.IfNull(call, nameof(call));
            this.failureCalls.Add(new CheckAndCall<TFailure>(check, call));
            return this;
        }

        IGuardFailure<TSuccess, TFailure, TOut> IGuardSuccessClosing<TSuccess, TFailure, TOut>.Failure()
        {
            return this;
        }

        IGuardSuccess<TSuccess, TFailure, TOut> IGuardFailureClosing<TSuccess, TFailure, TOut>.Success()
        {
            return this;
        }

        IGuardSuccessOpen<TSuccess, TFailure, TOut> IGuardEntryPoint<TSuccess, TFailure, TOut>.Success()
        {
            return this;
        }

        IGuardFailureOpen<TSuccess, TFailure, TOut> IGuardEntryPoint<TSuccess, TFailure, TOut>.Failure()
        {
            return this;
        }

        public TOut Do()
        {
            return this.innerResult.Do(
                success => CallFunctionOrDefault(success, this.successCalls, this.defaultSuccessCall),
                failure => CallFunctionOrDefault(failure, this.failureCalls, this.defaultFailureCall));
        }

        private static TOut CallFunctionOrDefault<T>(T obj, IEnumerable<CheckAndCall<T>> calls, Func<T, TOut> @default)
        {
            var matchingFunc = calls.FirstOrDefault(c => c.Check(obj));
            return matchingFunc.Call != null ? matchingFunc.Call(obj) : @default(obj);
        }

        private struct CheckAndCall<TIn>
        {
            public CheckAndCall(Func<TIn, bool> check, Func<TIn, TOut> call)
                : this()
            {
                this.Check = check;
                this.Call = call;
            }

            public Func<TIn, bool> Check { get; private set; }

            public Func<TIn, TOut> Call { get; private set; }
        }
    }
}