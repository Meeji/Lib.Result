// Is there a use case for this?
// If so makes more sense as the following two methods:
//   - Func<X> -> IEnumerable<Result<X, Exception>>
//   - Func<Result<T, U>> -> IEnumerable<Result<T, U>>

//namespace System1Group.Lib.Result
//{
//    using System;

//    public class RepeatingTryResult<TSuccess> : Result<TSuccess, Exception>
//    {
//        private readonly Func<TSuccess> factory;

//        public RepeatingTryResult(Func<TSuccess> factory)
//        {
//            this.factory = factory;
//        }

//        public override bool IsSuccess => this.TryRunFactory().IsSuccess;

//        public override TReturn Do<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<Exception, TReturn> onFailure)
//        {
//            return this.TryRunFactory().Do(onSuccess, onFailure);
//        }

//        private Result<TSuccess, Exception> TryRunFactory()
//        {
//            try
//            {
//                return this.factory();
//            }
//            catch (Exception ex)
//            {
//                return ex;
//            }
//        }
//    }
//}
