namespace Result;

public static class Result
{
    public static Result<TSuccess, TFailure> Success<TSuccess, TFailure>(TSuccess success) => new Success<TSuccess, TFailure>(success);

    public static Result<TSuccess, TFailure> Failure<TSuccess, TFailure>(TFailure failure) => new Failure<TSuccess, TFailure>(failure);

    public static Result<(T1, T2), TFailure> Pack<T1, T2, TFailure>(this Result<T1, TFailure> result1, Result<T2, TFailure> result2) => result1.MapToResult(r1 => result2.Map(r2 => (r1, r2)));

    public static Result<(T1, T2, T3), TFailure> Pack<T1, T2, T3, TFailure>(
        this Result<T1, TFailure> result1,
        Result<T2, TFailure> result2,
        Result<T3, TFailure> result3) => result1.MapToResult(r1 => result2.MapToResult(r2 => result3.Map(r3 => (r1, r2, r3))));

    public static Result<(T1, T2, T3, T4), TFailure> Pack<T1, T2, T3, T4, TFailure>(
        this Result<T1, TFailure> result1,
        Result<T2, TFailure> result2,
        Result<T3, TFailure> result3,
        Result<T4, TFailure> result4) => result1.MapToResult(r1 => result2.MapToResult(r2 => result3.MapToResult(r3 => result4.Map(r4 => (r1, r2, r3, r4)))));

    public static Result<(T1, T2, T3, T4, T5), TFailure> Pack<T1, T2, T3, T4, T5, TFailure>(
        this Result<T1, TFailure> result1,
        Result<T2, TFailure> result2,
        Result<T3, TFailure> result3,
        Result<T4, TFailure> result4,
        Result<T5, TFailure> result5) => result1.MapToResult(
            r1 => result2.MapToResult(
                r2 => result3.MapToResult(
                    r3 => result4.MapToResult(
                        r4 => result5.Map(
                            r5 => (r1, r2, r3, r4, r5))))));

    public static Result<(T1, T2, T3, T4, T5, T6), TFailure> Pack<T1, T2, T3, T4, T5, T6, TFailure>(
        this Result<T1, TFailure> result1,
        Result<T2, TFailure> result2,
        Result<T3, TFailure> result3,
        Result<T4, TFailure> result4,
        Result<T5, TFailure> result5,
        Result<T6, TFailure> result6) => result1.MapToResult(
            r1 => result2.MapToResult(
                r2 => result3.MapToResult(
                    r3 => result4.MapToResult(
                        r4 => result5.MapToResult(
                            r5 => result6.Map(
                                r6 => (r1, r2, r3, r4, r5, r6)))))));

    public static Result<(T1, T2, T3, T4, T5, T6, T7), TFailure> Pack<T1, T2, T3, T4, T5, T6, T7, TFailure>(
        this Result<T1, TFailure> result1,
        Result<T2, TFailure> result2,
        Result<T3, TFailure> result3,
        Result<T4, TFailure> result4,
        Result<T5, TFailure> result5,
        Result<T6, TFailure> result6,
        Result<T7, TFailure> result7) => result1.MapToResult(
            r1 => result2.MapToResult(
                r2 => result3.MapToResult(
                    r3 => result4.MapToResult(
                        r4 => result5.MapToResult(
                            r5 => result6.MapToResult(
                                r6 => result7.Map(
                                    r7 => (r1, r2, r3, r4, r5, r6, r7))))))));

    public static Result<(T1, T2, T3, T4, T5, T6, T7, T8), TFailure> Pack<T1, T2, T3, T4, T5, T6, T7, T8, TFailure>(
        this Result<T1, TFailure> result1,
        Result<T2, TFailure> result2,
        Result<T3, TFailure> result3,
        Result<T4, TFailure> result4,
        Result<T5, TFailure> result5,
        Result<T6, TFailure> result6,
        Result<T7, TFailure> result7,
        Result<T8, TFailure> result8) => result1.MapToResult(
            r1 => result2.MapToResult(
                r2 => result3.MapToResult(
                    r3 => result4.MapToResult(
                        r4 => result5.MapToResult(
                            r5 => result6.MapToResult(
                                r6 => result7.MapToResult(
                                    r7 => result8.Map(
                                        r8 => (r1, r2, r3, r4, r5, r6, r7, r8)))))))));

    public static Result<(T1, T2, T3, T4, T5, T6, T7, T8, T9), TFailure> Pack<T1, T2, T3, T4, T5, T6, T7, T8, T9, TFailure>(
        this Result<T1, TFailure> result1,
        Result<T2, TFailure> result2,
        Result<T3, TFailure> result3,
        Result<T4, TFailure> result4,
        Result<T5, TFailure> result5,
        Result<T6, TFailure> result6,
        Result<T7, TFailure> result7,
        Result<T8, TFailure> result8,
        Result<T9, TFailure> result9) => result1.MapToResult(
            r1 => result2.MapToResult(
                r2 => result3.MapToResult(
                    r3 => result4.MapToResult(
                        r4 => result5.MapToResult(
                            r5 => result6.MapToResult(
                                r6 => result7.MapToResult(
                                    r7 => result8.MapToResult(
                                        r8 => result9.Map(
                                            r9 => (r1, r2, r3, r4, r5, r6, r7, r8, r9))))))))));

    public static Result<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10), TFailure> Pack<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TFailure>(
        this Result<T1, TFailure> result1,
        Result<T2, TFailure> result2,
        Result<T3, TFailure> result3,
        Result<T4, TFailure> result4,
        Result<T5, TFailure> result5,
        Result<T6, TFailure> result6,
        Result<T7, TFailure> result7,
        Result<T8, TFailure> result8,
        Result<T9, TFailure> result9,
        Result<T10, TFailure> result10) => result1.MapToResult(
            r1 => result2.MapToResult(
                r2 => result3.MapToResult(
                    r3 => result4.MapToResult(
                        r4 => result5.MapToResult(
                            r5 => result6.MapToResult(
                                r6 => result7.MapToResult(
                                    r7 => result8.MapToResult(
                                        r8 => result9.MapToResult(
                                            r9 => result10.Map(
                                                r10 => (r1, r2, r3, r4, r5, r6, r7, r8, r9, r10)))))))))));

    public static Result<TSuccess, TFailure> CallWith<T1, TSuccess, TFailure>(
        this Func<T1, Result<TSuccess, TFailure>> func,
        Result<T1, TFailure> arg1) => arg1.MapToResult(func);
    
    public static Result<TSuccess, TFailure> CallWith<T1, T2, TSuccess, TFailure>(
        this Func<T1, T2, Result<TSuccess, TFailure>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2) => arg1.MapToResult(
            r1 => arg2.MapToResult(
                r2 => func(r1, r2)));
    
    public static Result<TSuccess, TFailure> CallWith<T1, T2, T3, TSuccess, TFailure>(
        this Func<T1, T2, T3, Result<TSuccess, TFailure>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3) => arg1.MapToResult(
            r1 => arg2.MapToResult(
                r2 => arg3.MapToResult(
                    r3 => func(r1, r2, r3))));
    
    public static Result<TSuccess, TFailure> CallWith<T1, T2, T3, T4, TSuccess, TFailure>(
        this Func<T1, T2, T3, T4, Result<TSuccess, TFailure>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3,
        Result<T4, TFailure> arg4) => arg1.MapToResult(
        r1 => arg2.MapToResult(
            r2 => arg3.MapToResult(
                r3 => arg4.MapToResult(
                    r4 => func(r1, r2, r3, r4)))));

    public static Result<TSuccess, TFailure> CallWith<T1, T2, T3, T4, T5, TSuccess, TFailure>(
        this Func<T1, T2, T3, T4, T5, Result<TSuccess, TFailure>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3,
        Result<T4, TFailure> arg4,
        Result<T5, TFailure> arg5) => arg1.MapToResult(
        r1 => arg2.MapToResult(
            r2 => arg3.MapToResult(
                r3 => arg4.MapToResult(
                    r4 => arg5.MapToResult(
                        r5 => func(r1, r2, r3, r4, r5))))));

    public static Result<TSuccess, TFailure> CallWith<T1, T2, T3, T4, T5, T6, TSuccess, TFailure>(
        this Func<T1, T2, T3, T4, T5, T6, Result<TSuccess, TFailure>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3,
        Result<T4, TFailure> arg4,
        Result<T5, TFailure> arg5,
        Result<T6, TFailure> arg6) => arg1.MapToResult(
        r1 => arg2.MapToResult(
            r2 => arg3.MapToResult(
                r3 => arg4.MapToResult(
                    r4 => arg5.MapToResult(
                        r5 => arg6.MapToResult(
                            r6 => func(r1, r2, r3, r4, r5, r6)))))));

public static Result<TSuccess, TFailure> CallWith<T1, T2, T3, T4, T5, T6, T7, TSuccess, TFailure>(
    this Func<T1, T2, T3, T4, T5, T6, T7, Result<TSuccess, TFailure>> func,
    Result<T1, TFailure> arg1,
    Result<T2, TFailure> arg2,
    Result<T3, TFailure> arg3,
    Result<T4, TFailure> arg4,
    Result<T5, TFailure> arg5,
    Result<T6, TFailure> arg6,
    Result<T7, TFailure> arg7) => arg1.MapToResult(
        r1 => arg2.MapToResult(
            r2 => arg3.MapToResult(
                r3 => arg4.MapToResult(
                    r4 => arg5.MapToResult(
                        r5 => arg6.MapToResult(
                            r6 => arg7.MapToResult(
                                r7 => func(r1, r2, r3, r4, r5, r6, r7))))))));

public static Result<TSuccess, TFailure> CallWith<T1, T2, T3, T4, T5, T6, T7, T8, TSuccess, TFailure>(
    this Func<T1, T2, T3, T4, T5, T6, T7, T8, Result<TSuccess, TFailure>> func,
    Result<T1, TFailure> arg1,
    Result<T2, TFailure> arg2,
    Result<T3, TFailure> arg3,
    Result<T4, TFailure> arg4,
    Result<T5, TFailure> arg5,
    Result<T6, TFailure> arg6,
    Result<T7, TFailure> arg7,
    Result<T8, TFailure> arg8) => arg1.MapToResult(
        r1 => arg2.MapToResult(
            r2 => arg3.MapToResult(
                r3 => arg4.MapToResult(
                    r4 => arg5.MapToResult(
                        r5 => arg6.MapToResult(
                            r6 => arg7.MapToResult(
                                r7 => arg8.MapToResult(
                                    r8 => func(r1, r2, r3, r4, r5, r6, r7, r8)))))))));

public static Result<TSuccess, TFailure> CallWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, TSuccess, TFailure>(
    this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Result<TSuccess, TFailure>> func,
    Result<T1, TFailure> arg1,
    Result<T2, TFailure> arg2,
    Result<T3, TFailure> arg3,
    Result<T4, TFailure> arg4,
    Result<T5, TFailure> arg5,
    Result<T6, TFailure> arg6,
    Result<T7, TFailure> arg7,
    Result<T8, TFailure> arg8,
    Result<T9, TFailure> arg9) => arg1.MapToResult(
        r1 => arg2.MapToResult(
            r2 => arg3.MapToResult(
                r3 => arg4.MapToResult(
                    r4 => arg5.MapToResult(
                        r5 => arg6.MapToResult(
                            r6 => arg7.MapToResult(
                                r7 => arg8.MapToResult(
                                    r8 => arg9.MapToResult(
                                        r9 => func(r1, r2, r3, r4, r5, r6, r7, r8, r9))))))))));

public static Result<TSuccess, TFailure> CallWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TSuccess, TFailure>(
    this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Result<TSuccess, TFailure>> func,
    Result<T1, TFailure> arg1,
    Result<T2, TFailure> arg2,
    Result<T3, TFailure> arg3,
    Result<T4, TFailure> arg4,
    Result<T5, TFailure> arg5,
    Result<T6, TFailure> arg6,
    Result<T7, TFailure> arg7,
    Result<T8, TFailure> arg8,
    Result<T9, TFailure> arg9,
    Result<T10, TFailure> arg10) => arg1.MapToResult(
        r1 => arg2.MapToResult(
            r2 => arg3.MapToResult(
                r3 => arg4.MapToResult(
                    r4 => arg5.MapToResult(
                        r5 => arg6.MapToResult(
                            r6 => arg7.MapToResult(
                                r7 => arg8.MapToResult(
                                    r8 => arg9.MapToResult(
                                        r9 => arg10.MapToResult(
                                            r10 => func(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10)))))))))));
}
