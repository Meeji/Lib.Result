﻿namespace Result.Exceptions;

using System;

public class InvalidUnwrapException : Exception
{
    private const string DefaultError =
        @"Tried to unwrap {0} value of a result when there was no such value. Try using .Do for safe handling of unexpected cases.
Value in result is: {1}";

    public InvalidUnwrapException(object result, object? item, UnwrapType failedUnwrapType, string customError)
        : base(customError)
    {
        this.Result = result;
        this.Item = item;
        this.FailedUnwrapType = failedUnwrapType;
    }

    public InvalidUnwrapException(object result, object? item, UnwrapType failedUnwrapType)
        : this(result, item, failedUnwrapType, string.Format(DefaultError, failedUnwrapType.ToString(), item?.ToString() ?? "NULL"))
    {
        this.Result = result;
        this.Item = item;
        this.FailedUnwrapType = failedUnwrapType;
    }

    public enum UnwrapType
    {
        Failure,
        Success
    }

    public object? Item { get; set; }

    public UnwrapType FailedUnwrapType { get; set; }

    public object Result { get; }
}
