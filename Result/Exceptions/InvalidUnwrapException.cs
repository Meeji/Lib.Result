namespace System1Group.Lib.Result.Exceptions
{
    using System;

    internal class InvalidUnwrapException : Exception
    {
        private const string DefaultError =
            @"Tried to unwrap {0} value of a result when there was no such value. Try using .Do for safe handling of unexpected cases.
        Action value is {1}";

        public InvalidUnwrapException(object result, object item, UnwrapType failedUnwrapType, string customError = null)
            : base(
                !string.IsNullOrWhiteSpace(customError)
                    ? customError
                    : string.Format(DefaultError, failedUnwrapType.ToString(), item?.ToString() ?? "NULL"))
        {
            this.Result = result;
            this.Item = item;
            this.FailedUnwrapType = failedUnwrapType;
        }

        internal enum UnwrapType
        {
            Failure,
            Success
        }

        public object Item { get; set; }

        public UnwrapType FailedUnwrapType { get; set; }

        public object Result { get; }
    }
}
