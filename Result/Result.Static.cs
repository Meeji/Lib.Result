namespace Result;

public static class Result
{
    public static Result<TSuccess, TFailure> Success<TSuccess, TFailure>(TSuccess success) => new Success<TSuccess, TFailure>(success);

    public static Result<TSuccess, TFailure> Failure<TSuccess, TFailure>(TFailure failure) => new Failure<TSuccess, TFailure>(failure);

    public static Result<(T1, T2), TFailure> And<T1, T2, TFailure>(this Result<T1, TFailure> result1, Result<T2, TFailure> result2) => result1.Then(r1 => result2.Then(r2 => (r1, r2)));

    public static Result<(T1, T2, T3), TFailure> And<T1, T2, T3, TFailure>(
        this Result<T1, TFailure> result1,
        Result<T2, TFailure> result2,
        Result<T3, TFailure> result3) => result1.Then(r1 => result2.Then(r2 => result3.Then<(T1 r1, T2 r2, T3 r3)>(r3 => (r1, r2, r3))));

    public static Result<(T1, T2, T3, T4), TFailure> And<T1, T2, T3, T4, TFailure>(
        this Result<T1, TFailure> result1,
        Result<T2, TFailure> result2,
        Result<T3, TFailure> result3,
        Result<T4, TFailure> result4) => result1.Then(r1 => result2.Then(r2 => result3.Then(r3 => result4.Then<(T1 r1, T2 r2, T3 r3, T4 r4)>(r4 => (r1, r2, r3, r4)))));

    public static Result<(T1, T2, T3, T4, T5), TFailure> And<T1, T2, T3, T4, T5, TFailure>(
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

    public static Result<(T1, T2, T3, T4, T5, T6), TFailure> And<T1, T2, T3, T4, T5, T6, TFailure>(
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

    public static Result<(T1, T2, T3, T4, T5, T6, T7), TFailure> And<T1, T2, T3, T4, T5, T6, T7, TFailure>(
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

    public static Result<(T1, T2, T3, T4, T5, T6, T7, T8), TFailure> And<T1, T2, T3, T4, T5, T6, T7, T8, TFailure>(
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

    public static Result<(T1, T2, T3, T4, T5, T6, T7, T8, T9), TFailure> And<T1, T2, T3, T4, T5, T6, T7, T8, T9, TFailure>(
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

    public static Result<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10), TFailure> And<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TFailure>(
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

    public static Result<TOutput, TFailure> Into<T1, T2, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2), TFailure> result,
        Func<T1, T2, Result<TOutput, TFailure>> func) =>
        result.Then(i => func(i.r1, i.r2));
    
    public static Result<TOutput, TFailure> Into<T1, T2, T3, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2, T3 r3), TFailure> result,
        Func<T1, T2, T3, Result<TOutput, TFailure>> func) =>
        result.Then(i => func(i.r1, i.r2, i.r3));
    
    public static Result<TOutput, TFailure> Into<T1, T2, T3, T4, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2, T3 r3, T4 r4), TFailure> result,
        Func<T1, T2, T3, T4, Result<TOutput, TFailure>> func) =>
        result.Then(i => func(i.r1, i.r2, i.r3, i.r4));

    public static Result<TOutput, TFailure> Into<T1, T2, T3, T4, T5, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5), TFailure> result,
        Func<T1, T2, T3, T4, T5, Result<TOutput, TFailure>> func) =>
        result.Then(i => func(i.r1, i.r2, i.r3, i.r4, i.r5));

    public static Result<TOutput, TFailure> Into<T1, T2, T3, T4, T5, T6, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5, T6 r6), TFailure> result,
        Func<T1, T2, T3, T4, T5, T6, Result<TOutput, TFailure>> func) =>
        result.Then(i => func(i.r1, i.r2, i.r3, i.r4, i.r5, i.r6));

    public static Result<TOutput, TFailure> Into<T1, T2, T3, T4, T5, T6, T7, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5, T6 r6, T7 r7), TFailure> result,
        Func<T1, T2, T3, T4, T5, T6, T7, Result<TOutput, TFailure>> func) =>
        result.Then(i => func(i.r1, i.r2, i.r3, i.r4, i.r5, i.r6, i.r7));

    public static Result<TOutput, TFailure> Into<T1, T2, T3, T4, T5, T6, T7, T8, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5, T6 r6, T7 r7, T8 r8), TFailure> result,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, Result<TOutput, TFailure>> func) =>
        result.Then(i => func(i.r1, i.r2, i.r3, i.r4, i.r5, i.r6, i.r7, i.r8));

    public static Result<TOutput, TFailure> Into<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5, T6 r6, T7 r7, T8 r8, T9 r9), TFailure> result,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Result<TOutput, TFailure>> func) =>
        result.Then(i => func(i.r1, i.r2, i.r3, i.r4, i.r5, i.r6, i.r7, i.r8, i.r9));

    public static Result<TOutput, TFailure> Into<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5, T6 r6, T7 r7, T8 r8, T9 r9, T10 r10), TFailure> result,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Result<TOutput, TFailure>> func) =>
        result.Then(i => func(i.r1, i.r2, i.r3, i.r4, i.r5, i.r6, i.r7, i.r8, i.r9, i.r10));
    
    public static Task<Result<TOutput, TFailure>> IntoAsync<T1, T2, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2), TFailure> result,
        Func<T1, T2, Task<Result<TOutput, TFailure>>> func) =>
        result.ThenAsync(i => func(i.r1, i.r2));
    
    public static Task<Result<TOutput, TFailure>> IntoAsync<T1, T2, T3, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2, T3 r3), TFailure> result,
        Func<T1, T2, T3, Task<Result<TOutput, TFailure>>> func) =>
        result.ThenAsync(i => func(i.r1, i.r2, i.r3));
    
    public static Task<Result<TOutput, TFailure>> IntoAsync<T1, T2, T3, T4, TOutput, TFailure>(
    this Result<(T1 r1, T2 r2, T3 r3, T4 r4), TFailure> result,
    Func<T1, T2, T3, T4, Task<Result<TOutput, TFailure>>> func) =>
    result.ThenAsync(i => func(i.r1, i.r2, i.r3, i.r4));

    public static Task<Result<TOutput, TFailure>> IntoAsync<T1, T2, T3, T4, T5, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5), TFailure> result,
        Func<T1, T2, T3, T4, T5, Task<Result<TOutput, TFailure>>> func) =>
        result.ThenAsync(i => func(i.r1, i.r2, i.r3, i.r4, i.r5));

    public static Task<Result<TOutput, TFailure>> IntoAsync<T1, T2, T3, T4, T5, T6, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5, T6 r6), TFailure> result,
        Func<T1, T2, T3, T4, T5, T6, Task<Result<TOutput, TFailure>>> func) =>
        result.ThenAsync(i => func(i.r1, i.r2, i.r3, i.r4, i.r5, i.r6));

    public static Task<Result<TOutput, TFailure>> IntoAsync<T1, T2, T3, T4, T5, T6, T7, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5, T6 r6, T7 r7), TFailure> result,
        Func<T1, T2, T3, T4, T5, T6, T7, Task<Result<TOutput, TFailure>>> func) =>
        result.ThenAsync(i => func(i.r1, i.r2, i.r3, i.r4, i.r5, i.r6, i.r7));

    public static Task<Result<TOutput, TFailure>> IntoAsync<T1, T2, T3, T4, T5, T6, T7, T8, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5, T6 r6, T7 r7, T8 r8), TFailure> result,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, Task<Result<TOutput, TFailure>>> func) =>
        result.ThenAsync(i => func(i.r1, i.r2, i.r3, i.r4, i.r5, i.r6, i.r7, i.r8));

    public static Task<Result<TOutput, TFailure>> IntoAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5, T6 r6, T7 r7, T8 r8, T9 r9), TFailure> result,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task<Result<TOutput, TFailure>>> func) =>
        result.ThenAsync(i => func(i.r1, i.r2, i.r3, i.r4, i.r5, i.r6, i.r7, i.r8, i.r9));

    public static Task<Result<TOutput, TFailure>> IntoAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOutput, TFailure>(
        this Result<(T1 r1, T2 r2, T3 r3, T4 r4, T5 r5, T6 r6, T7 r7, T8 r8, T9 r9, T10 r10), TFailure> result,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task<Result<TOutput, TFailure>>> func) =>
        result.ThenAsync(i => func(i.r1, i.r2, i.r3, i.r4, i.r5, i.r6, i.r7, i.r8, i.r9, i.r10));
}
