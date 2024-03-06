namespace Result;

public static class Result
{
    public static Result<TSuccess, TFailure> Success<TSuccess, TFailure>(TSuccess success) => new Success<TSuccess, TFailure>(success);

    public static Result<TSuccess, TFailure> Failure<TSuccess, TFailure>(TFailure failure) => new Failure<TSuccess, TFailure>(failure);

    public static Result<(T1, T2), TFailure> Pack<T1, T2, TFailure>(this Result<T1, TFailure> result1, Result<T2, TFailure> result2) => result1.Then(r1 => result2.Then(r2 => (r1, r2)));

    public static Result<(T1, T2, T3), TFailure> Pack<T1, T2, T3, TFailure>(
        this Result<T1, TFailure> result1,
        Result<T2, TFailure> result2,
        Result<T3, TFailure> result3) => result1.Then(r1 => result2.Then(r2 => result3.Then<(T1 r1, T2 r2, T3 r3)>(r3 => (r1, r2, r3))));

    public static Result<(T1, T2, T3, T4), TFailure> Pack<T1, T2, T3, T4, TFailure>(
        this Result<T1, TFailure> result1,
        Result<T2, TFailure> result2,
        Result<T3, TFailure> result3,
        Result<T4, TFailure> result4) => result1.Then(r1 => result2.Then(r2 => result3.Then(r3 => result4.Then<(T1 r1, T2 r2, T3 r3, T4 r4)>(r4 => (r1, r2, r3, r4)))));

    public static Result<(T1, T2, T3, T4, T5), TFailure> Pack<T1, T2, T3, T4, T5, TFailure>(
        this Result<T1, TFailure> result1,
        Result<T2, TFailure> result2,
        Result<T3, TFailure> result3,
        Result<T4, TFailure> result4,
        Result<T5, TFailure> result5) => result1.Then(
            r1 => result2.Then(
                r2 => result3.Then(
                    r3 => result4.Then(
                        r4 => result5.Then<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5)>(
                            r5 => (r1, r2, r3, r4, r5))))));

    public static Result<(T1, T2, T3, T4, T5, T6), TFailure> Pack<T1, T2, T3, T4, T5, T6, TFailure>(
        this Result<T1, TFailure> result1,
        Result<T2, TFailure> result2,
        Result<T3, TFailure> result3,
        Result<T4, TFailure> result4,
        Result<T5, TFailure> result5,
        Result<T6, TFailure> result6) => result1.Then(
            r1 => result2.Then(
                r2 => result3.Then(
                    r3 => result4.Then(
                        r4 => result5.Then(
                            r5 => result6.Then<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5, T6 r6)>(
                                r6 => (r1, r2, r3, r4, r5, r6)))))));

    public static Result<(T1, T2, T3, T4, T5, T6, T7), TFailure> Pack<T1, T2, T3, T4, T5, T6, T7, TFailure>(
        this Result<T1, TFailure> result1,
        Result<T2, TFailure> result2,
        Result<T3, TFailure> result3,
        Result<T4, TFailure> result4,
        Result<T5, TFailure> result5,
        Result<T6, TFailure> result6,
        Result<T7, TFailure> result7) => result1.Then(
            r1 => result2.Then(
                r2 => result3.Then(
                    r3 => result4.Then(
                        r4 => result5.Then(
                            r5 => result6.Then(
                                r6 => result7.Then<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5, T6 r6, T7 r7)>(
                                    r7 => (r1, r2, r3, r4, r5, r6, r7))))))));

    public static Result<(T1, T2, T3, T4, T5, T6, T7, T8), TFailure> Pack<T1, T2, T3, T4, T5, T6, T7, T8, TFailure>(
        this Result<T1, TFailure> result1,
        Result<T2, TFailure> result2,
        Result<T3, TFailure> result3,
        Result<T4, TFailure> result4,
        Result<T5, TFailure> result5,
        Result<T6, TFailure> result6,
        Result<T7, TFailure> result7,
        Result<T8, TFailure> result8) => result1.Then(
            r1 => result2.Then(
                r2 => result3.Then(
                    r3 => result4.Then(
                        r4 => result5.Then(
                            r5 => result6.Then(
                                r6 => result7.Then(
                                    r7 => result8.Then<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5, T6 r6, T7 r7, T8 r8)>(
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
        Result<T9, TFailure> result9) => result1.Then(
            r1 => result2.Then(
                r2 => result3.Then(
                    r3 => result4.Then(
                        r4 => result5.Then(
                            r5 => result6.Then(
                                r6 => result7.Then(
                                    r7 => result8.Then(
                                        r8 => result9.Then<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5, T6 r6, T7 r7, T8 r8, T9 r9)>(
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
        Result<T10, TFailure> result10) => result1.Then(
            r1 => result2.Then(
                r2 => result3.Then(
                    r3 => result4.Then(
                        r4 => result5.Then(
                            r5 => result6.Then(
                                r6 => result7.Then(
                                    r7 => result8.Then(
                                        r8 => result9.Then(
                                            r9 => result10.Then<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5, T6 r6, T7 r7, T8 r8, T9 r9, T10 r10)>(
                                                r10 => (r1, r2, r3, r4, r5, r6, r7, r8, r9, r10)))))))))));

    public static Result<TSuccess, TFailure> CallWith<T1, TSuccess, TFailure>(
        this Func<T1, Result<TSuccess, TFailure>> func,
        Result<T1, TFailure> arg1) => arg1.Then(func);
    
    public static Result<TSuccess, TFailure> CallWith<T1, T2, TSuccess, TFailure>(
        this Func<T1, T2, Result<TSuccess, TFailure>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2) => arg1.Then(
            r1 => arg2.Then(
                r2 => func(r1, r2)));
    
    public static Result<TSuccess, TFailure> CallWith<T1, T2, T3, TSuccess, TFailure>(
        this Func<T1, T2, T3, Result<TSuccess, TFailure>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3) => arg1.Then(
            r1 => arg2.Then(
                r2 => arg3.Then(
                    r3 => func(r1, r2, r3))));
    
    public static Result<TSuccess, TFailure> CallWith<T1, T2, T3, T4, TSuccess, TFailure>(
        this Func<T1, T2, T3, T4, Result<TSuccess, TFailure>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3,
        Result<T4, TFailure> arg4) => arg1.Then(
        r1 => arg2.Then(
            r2 => arg3.Then(
                r3 => arg4.Then(
                    r4 => func(r1, r2, r3, r4)))));

    public static Result<TSuccess, TFailure> CallWith<T1, T2, T3, T4, T5, TSuccess, TFailure>(
        this Func<T1, T2, T3, T4, T5, Result<TSuccess, TFailure>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3,
        Result<T4, TFailure> arg4,
        Result<T5, TFailure> arg5) => arg1.Then(
        r1 => arg2.Then(
            r2 => arg3.Then(
                r3 => arg4.Then(
                    r4 => arg5.Then(
                        r5 => func(r1, r2, r3, r4, r5))))));

    public static Result<TSuccess, TFailure> CallWith<T1, T2, T3, T4, T5, T6, TSuccess, TFailure>(
        this Func<T1, T2, T3, T4, T5, T6, Result<TSuccess, TFailure>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3,
        Result<T4, TFailure> arg4,
        Result<T5, TFailure> arg5,
        Result<T6, TFailure> arg6) => arg1.Then(
        r1 => arg2.Then(
            r2 => arg3.Then(
                r3 => arg4.Then(
                    r4 => arg5.Then(
                        r5 => arg6.Then(
                            r6 => func(r1, r2, r3, r4, r5, r6)))))));

    public static Result<TSuccess, TFailure> CallWith<T1, T2, T3, T4, T5, T6, T7, TSuccess, TFailure>(
        this Func<T1, T2, T3, T4, T5, T6, T7, Result<TSuccess, TFailure>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3,
        Result<T4, TFailure> arg4,
        Result<T5, TFailure> arg5,
        Result<T6, TFailure> arg6,
        Result<T7, TFailure> arg7) => arg1.Then(
            r1 => arg2.Then(
                r2 => arg3.Then(
                    r3 => arg4.Then(
                        r4 => arg5.Then(
                            r5 => arg6.Then(
                                r6 => arg7.Then(
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
        Result<T8, TFailure> arg8) => arg1.Then(
            r1 => arg2.Then(
                r2 => arg3.Then(
                    r3 => arg4.Then(
                        r4 => arg5.Then(
                            r5 => arg6.Then(
                                r6 => arg7.Then(
                                    r7 => arg8.Then(
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
        Result<T9, TFailure> arg9) => arg1.Then(
            r1 => arg2.Then(
                r2 => arg3.Then(
                    r3 => arg4.Then(
                        r4 => arg5.Then(
                            r5 => arg6.Then(
                                r6 => arg7.Then(
                                    r7 => arg8.Then(
                                        r8 => arg9.Then(
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
        Result<T10, TFailure> arg10) => arg1.Then(
            r1 => arg2.Then(
                r2 => arg3.Then(
                    r3 => arg4.Then(
                        r4 => arg5.Then(
                            r5 => arg6.Then(
                                r6 => arg7.Then(
                                    r7 => arg8.Then(
                                        r8 => arg9.Then(
                                            r9 => arg10.Then(
                                                r10 => func(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10)))))))))));

    public static Task<Result<TSuccess, TFailure>> CallWithAsync<T1, TSuccess, TFailure>(
            this Func<T1, Task<Result<TSuccess, TFailure>>> func,
            Result<T1, TFailure> arg1) => arg1.ThenAsync(func);
        
    public static Task<Result<TSuccess, TFailure>> CallWithAsync<T1, T2, TSuccess, TFailure>(
        this Func<T1, T2, Task<Result<TSuccess, TFailure>>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2) => arg1.ThenAsync(
            r1 => arg2.ThenAsync(
                r2 => func(r1, r2)));
    
    public static Task<Result<TSuccess, TFailure>> CallWithAsync<T1, T2, T3, TSuccess, TFailure>(
        this Func<T1, T2, T3, Task<Result<TSuccess, TFailure>>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3) => arg1.ThenAsync(
            r1 => arg2.ThenAsync(
                r2 => arg3.ThenAsync(
                    r3 => func(r1, r2, r3))));
    
    public static Task<Result<TSuccess, TFailure>> CallWithAsync<T1, T2, T3, T4, TSuccess, TFailure>(
        this Func<T1, T2, T3, T4, Task<Result<TSuccess, TFailure>>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3,
        Result<T4, TFailure> arg4) => arg1.ThenAsync(
        r1 => arg2.ThenAsync(
            r2 => arg3.ThenAsync(
                r3 => arg4.ThenAsync(
                    r4 => func(r1, r2, r3, r4)))));

    public static Task<Result<TSuccess, TFailure>> CallWithAsync<T1, T2, T3, T4, T5, TSuccess, TFailure>(
        this Func<T1, T2, T3, T4, T5, Task<Result<TSuccess, TFailure>>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3,
        Result<T4, TFailure> arg4,
        Result<T5, TFailure> arg5) => arg1.ThenAsync(
        r1 => arg2.ThenAsync(
            r2 => arg3.ThenAsync(
                r3 => arg4.ThenAsync(
                    r4 => arg5.ThenAsync(
                        r5 => func(r1, r2, r3, r4, r5))))));

    public static Task<Result<TSuccess, TFailure>> CallWithAsync<T1, T2, T3, T4, T5, T6, TSuccess, TFailure>(
        this Func<T1, T2, T3, T4, T5, T6, Task<Result<TSuccess, TFailure>>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3,
        Result<T4, TFailure> arg4,
        Result<T5, TFailure> arg5,
        Result<T6, TFailure> arg6) => arg1.ThenAsync(
        r1 => arg2.ThenAsync(
            r2 => arg3.ThenAsync(
                r3 => arg4.ThenAsync(
                    r4 => arg5.ThenAsync(
                        r5 => arg6.ThenAsync(
                            r6 => func(r1, r2, r3, r4, r5, r6)))))));

    public static Task<Result<TSuccess, TFailure>> CallWithAsync<T1, T2, T3, T4, T5, T6, T7, TSuccess, TFailure>(
        this Func<T1, T2, T3, T4, T5, T6, T7, Task<Result<TSuccess, TFailure>>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3,
        Result<T4, TFailure> arg4,
        Result<T5, TFailure> arg5,
        Result<T6, TFailure> arg6,
        Result<T7, TFailure> arg7) => arg1.ThenAsync(
            r1 => arg2.ThenAsync(
                r2 => arg3.ThenAsync(
                    r3 => arg4.ThenAsync(
                        r4 => arg5.ThenAsync(
                            r5 => arg6.ThenAsync(
                                r6 => arg7.ThenAsync(
                                    r7 => func(r1, r2, r3, r4, r5, r6, r7))))))));

    public static Task<Result<TSuccess, TFailure>> CallWithAsync<T1, T2, T3, T4, T5, T6, T7, T8, TSuccess, TFailure>(
        this Func<T1, T2, T3, T4, T5, T6, T7, T8, Task<Result<TSuccess, TFailure>>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3,
        Result<T4, TFailure> arg4,
        Result<T5, TFailure> arg5,
        Result<T6, TFailure> arg6,
        Result<T7, TFailure> arg7,
        Result<T8, TFailure> arg8) => arg1.ThenAsync(
            r1 => arg2.ThenAsync(
                r2 => arg3.ThenAsync(
                    r3 => arg4.ThenAsync(
                        r4 => arg5.ThenAsync(
                            r5 => arg6.ThenAsync(
                                r6 => arg7.ThenAsync(
                                    r7 => arg8.ThenAsync(
                                        r8 => func(r1, r2, r3, r4, r5, r6, r7, r8)))))))));

    public static Task<Result<TSuccess, TFailure>> CallWithAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TSuccess, TFailure>(
        this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task<Result<TSuccess, TFailure>>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3,
        Result<T4, TFailure> arg4,
        Result<T5, TFailure> arg5,
        Result<T6, TFailure> arg6,
        Result<T7, TFailure> arg7,
        Result<T8, TFailure> arg8,
        Result<T9, TFailure> arg9) => arg1.ThenAsync(
            r1 => arg2.ThenAsync(
                r2 => arg3.ThenAsync(
                    r3 => arg4.ThenAsync(
                        r4 => arg5.ThenAsync(
                            r5 => arg6.ThenAsync(
                                r6 => arg7.ThenAsync(
                                    r7 => arg8.ThenAsync(
                                        r8 => arg9.ThenAsync(
                                            r9 => func(r1, r2, r3, r4, r5, r6, r7, r8, r9))))))))));

    public static Task<Result<TSuccess, TFailure>> CallWithAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TSuccess, TFailure>(
        this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task<Result<TSuccess, TFailure>>> func,
        Result<T1, TFailure> arg1,
        Result<T2, TFailure> arg2,
        Result<T3, TFailure> arg3,
        Result<T4, TFailure> arg4,
        Result<T5, TFailure> arg5,
        Result<T6, TFailure> arg6,
        Result<T7, TFailure> arg7,
        Result<T8, TFailure> arg8,
        Result<T9, TFailure> arg9,
        Result<T10, TFailure> arg10) => arg1.ThenAsync(
            r1 => arg2.ThenAsync(
                r2 => arg3.ThenAsync(
                    r3 => arg4.ThenAsync(
                        r4 => arg5.ThenAsync(
                            r5 => arg6.ThenAsync(
                                r6 => arg7.ThenAsync(
                                    r7 => arg8.ThenAsync(
                                        r8 => arg9.ThenAsync(
                                            r9 => arg10.ThenAsync(
                                                r10 => func(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10)))))))))));

    public static Result<TOutput, TFailure> Into<T1, T2, TFailure, TOutput>(
        this (Result<T1, TFailure> r1, Result<T2, TFailure> r2) results,
        Func<T1, T2, Result<TOutput, TFailure>> func) =>
        CallWith(func, results.r1, results.r2);
    
    public static Result<TOutput, TFailure> Into<T1, T2, T3, TFailure, TOutput>(
        this (Result<T1, TFailure> r1, Result<T2, TFailure> r2, Result<T3, TFailure> r3) results,
        Func<T1, T2, T3, Result<TOutput, TFailure>> func) =>
        CallWith(func, results.r1, results.r2, results.r3);
    
    public static Result<TOutput, TFailure> Into<T1, T2, T3, T4, TFailure, TOutput>(
        this (Result<T1, TFailure> r1, Result<T2, TFailure> r2, Result<T3, TFailure> r3, Result<T4, TFailure> r4) results,
        Func<T1, T2, T3, T4, Result<TOutput, TFailure>> func) =>
        CallWith(func, results.r1, results.r2, results.r3, results.r4);

    public static Result<TOutput, TFailure> Into<T1, T2, T3, T4, T5, TFailure, TOutput>(
        this (Result<T1, TFailure> r1, Result<T2, TFailure> r2, Result<T3, TFailure> r3, Result<T4, TFailure> r4, Result<T5, TFailure> r5) results,
        Func<T1, T2, T3, T4, T5, Result<TOutput, TFailure>> func) =>
        CallWith(func, results.r1, results.r2, results.r3, results.r4, results.r5);

    public static Result<TOutput, TFailure> Into<T1, T2, T3, T4, T5, T6, TFailure, TOutput>(
        this (Result<T1, TFailure> r1, Result<T2, TFailure> r2, Result<T3, TFailure> r3, Result<T4, TFailure> r4, Result<T5, TFailure> r5, Result<T6, TFailure> r6) results,
        Func<T1, T2, T3, T4, T5, T6, Result<TOutput, TFailure>> func) =>
        CallWith(func, results.r1, results.r2, results.r3, results.r4, results.r5, results.r6);

    public static Result<TOutput, TFailure> Into<T1, T2, T3, T4, T5, T6, T7, TFailure, TOutput>(
        this (Result<T1, TFailure> r1, Result<T2, TFailure> r2, Result<T3, TFailure> r3, Result<T4, TFailure> r4, Result<T5, TFailure> r5, Result<T6, TFailure> r6, Result<T7, TFailure> r7) results,
        Func<T1, T2, T3, T4, T5, T6, T7, Result<TOutput, TFailure>> func) =>
        CallWith(func, results.r1, results.r2, results.r3, results.r4, results.r5, results.r6, results.r7);

    public static Result<TOutput, TFailure> Into<T1, T2, T3, T4, T5, T6, T7, T8, TFailure, TOutput>(
        this (Result<T1, TFailure> r1, Result<T2, TFailure> r2, Result<T3, TFailure> r3, Result<T4, TFailure> r4, Result<T5, TFailure> r5, Result<T6, TFailure> r6, Result<T7, TFailure> r7, Result<T8, TFailure> r8) results,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, Result<TOutput, TFailure>> func) =>
        CallWith(func, results.r1, results.r2, results.r3, results.r4, results.r5, results.r6, results.r7, results.r8);

    public static Result<TOutput, TFailure> Into<T1, T2, T3, T4, T5, T6, T7, T8, T9, TFailure, TOutput>(
        this (Result<T1, TFailure> r1, Result<T2, TFailure> r2, Result<T3, TFailure> r3, Result<T4, TFailure> r4, Result<T5, TFailure> r5, Result<T6, TFailure> r6, Result<T7, TFailure> r7, Result<T8, TFailure> r8, Result<T9, TFailure> r9) results,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Result<TOutput, TFailure>> func) =>
        CallWith(func, results.r1, results.r2, results.r3, results.r4, results.r5, results.r6, results.r7, results.r8, results.r9);
    
    public static Result<TOutput, TFailure> Into<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TFailure, TOutput>(
        this (Result<T1, TFailure> r1, Result<T2, TFailure> r2, Result<T3, TFailure> r3, Result<T4, TFailure> r4, Result<T5, TFailure> r5, Result<T6, TFailure> r6, Result<T7, TFailure> r7, Result<T8, TFailure> r8, Result<T9, TFailure> r9, Result<T10, TFailure> r10) results,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Result<TOutput, TFailure>> func) =>
        CallWith(func, results.r1, results.r2, results.r3, results.r4, results.r5, results.r6, results.r7, results.r8, results.r9, results.r10);
}
