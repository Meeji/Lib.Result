namespace System1Group.Lib.Result.Exceptions
{
    using System;

    internal class InvalidUnwrapException : Exception
    {
        public InvalidUnwrapException(object result, UnwrapType failedUnwrapType, string customError = null)
            : base(
                !string.IsNullOrWhiteSpace(customError)
                    ? customError
                    : $"Tried to unwrap {failedUnwrapType.ToString()} value of a result when there was no such value. Try using .Do for safe handling of unexpected cases.")
        {
            this.Result = result;
            this.FailedUnwrapType = failedUnwrapType;
        }

        internal enum UnwrapType
        {
            Failure,
            Success
        }

        public UnwrapType FailedUnwrapType { get; set; }

        public object Result { get; }
    }
}
