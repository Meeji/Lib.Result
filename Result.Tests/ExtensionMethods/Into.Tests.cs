using NUnit.Framework;
using Result.Unsafe;
using Tuple2 = (int, int);
using Tuple3 = (int, int, int);
using Tuple4 = (int, int, int, int);
using Tuple5 = (int, int, int, int, int);
using Tuple6 = (int, int, int, int, int, int);
using Tuple7 = (int, int, int, int, int, int, int);
using Tuple8 = (int, int, int, int, int, int, int, int);
using Tuple9 = (int, int, int, int, int, int, int, int, int);
using Tuple10 = (int, int, int, int, int, int, int, int, int, int);

namespace Result.Tests;

[TestFixture]
public class Into_Tests
{
    private Result<int[], string> Success2(int i1, int i2) => new[] { i1, i2 };
    private Result<int[], string> Success3(int i1, int i2, int i3) => new[] { i1, i2, i3 };
    private Result<int[], string> Success4(int i1, int i2, int i3, int i4) => new[] { i1, i2, i3, i4 };
    private Result<int[], string> Success5(int i1, int i2, int i3, int i4, int i5) => new[] { i1, i2, i3, i4, i5 };
    private Result<int[], string> Success6(int i1, int i2, int i3, int i4, int i5, int i6) => new[] { i1, i2, i3, i4, i5, i6 };
    private Result<int[], string> Success7(int i1, int i2, int i3, int i4, int i5, int i6, int i7) => new[] { i1, i2, i3, i4, i5, i6, i7 };
    private Result<int[], string> Success8(int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8) => new[] { i1, i2, i3, i4, i5, i6, i7, i8 };
    private Result<int[], string> Success9(int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9) => new[] { i1, i2, i3, i4, i5, i6, i7, i8, i9 };
    private Result<int[], string> Success10(int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9, int i10) => new[] { i1, i2, i3, i4, i5, i6, i7, i8, i9, i10 };
    private Result<int[], string> Failure2(int i1, int i2) => "error";
    private Result<int[], string> Failure3(int i1, int i2, int i3) => "error";
    private Result<int[], string> Failure4(int i1, int i2, int i3, int i4) => "error";
    private Result<int[], string> Failure5(int i1, int i2, int i3, int i4, int i5) => "error";
    private Result<int[], string> Failure6(int i1, int i2, int i3, int i4, int i5, int i6) => "error";
    private Result<int[], string> Failure7(int i1, int i2, int i3, int i4, int i5, int i6, int i7) => "error";
    private Result<int[], string> Failure8(int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8) => "error";
    private Result<int[], string> Failure9(int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9) => "error";
    private Result<int[], string> Failure10(int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9, int i10) => "error";

    [Test]
    public void Ok_All()
    {
        var result2 = Result.Success<Tuple2, string>((1, 2));
        var result3 = Result.Success<Tuple3, string>((1, 2, 3));
        var result4 = Result.Success<Tuple4, string>((1, 2, 3, 4));
        var result5 = Result.Success<Tuple5, string>((1, 2, 3, 4, 5));
        var result6 = Result.Success<Tuple6, string>((1, 2, 3, 4, 5, 6));
        var result7 = Result.Success<Tuple7, string>((1, 2, 3, 4, 5, 6, 7));
        var result8 = Result.Success<Tuple8, string>((1, 2, 3, 4, 5, 6, 7, 8));
        var result9 = Result.Success<Tuple9, string>((1, 2, 3, 4, 5, 6, 7, 8, 9));
        var result10 = Result.Success<Tuple10, string>((1, 2, 3, 4, 5, 6, 7, 8, 9, 10));
        Assert.Multiple(() =>
        {
            Assert.That(result2.Then(Success2).Unwrap(), Is.EqualTo(new[] { 1, 2 }));
            Assert.That(result3.Then(Success3).Unwrap(), Is.EqualTo(new[] { 1, 2, 3 }));
            Assert.That(result4.Then(Success4).Unwrap(), Is.EqualTo(new[] { 1, 2, 3, 4 }));
            Assert.That(result5.Then(Success5).Unwrap(), Is.EqualTo(new[] { 1, 2, 3, 4, 5 }));
            Assert.That(result6.Then(Success6).Unwrap(), Is.EqualTo(new[] { 1, 2, 3, 4, 5, 6 }));
            Assert.That(result7.Then(Success7).Unwrap(), Is.EqualTo(new[] { 1, 2, 3, 4, 5, 6, 7 }));
            Assert.That(result8.Then(Success8).Unwrap(), Is.EqualTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8 }));
            Assert.That(result9.Then(Success9).Unwrap(), Is.EqualTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
            Assert.That(result10.Then(Success10).Unwrap(), Is.EqualTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }));
        });
    }
    
    [Test]
    public void StartWithFailure_All()
    {
        var result2 = Result.Failure<Tuple2, string>("foo");
        var result3 = Result.Failure<Tuple3, string>("foo");
        var result4 = Result.Failure<Tuple4, string>("foo");
        var result5 = Result.Failure<Tuple5, string>("foo");
        var result6 = Result.Failure<Tuple6, string>("foo");
        var result7 = Result.Failure<Tuple7, string>("foo");
        var result8 = Result.Failure<Tuple8, string>("foo");
        var result9 = Result.Failure<Tuple9, string>("foo");
        var result10 = Result.Failure<Tuple10, string>("foo");
        Assert.Multiple(() =>
        {
            Assert.That(result2.Then(Success2).UnwrapError(), Is.EqualTo("foo"));
            Assert.That(result3.Then(Success3).UnwrapError(), Is.EqualTo("foo"));
            Assert.That(result4.Then(Success4).UnwrapError(), Is.EqualTo("foo"));
            Assert.That(result5.Then(Success5).UnwrapError(), Is.EqualTo("foo"));
            Assert.That(result6.Then(Success6).UnwrapError(), Is.EqualTo("foo"));
            Assert.That(result7.Then(Success7).UnwrapError(), Is.EqualTo("foo"));
            Assert.That(result8.Then(Success8).UnwrapError(), Is.EqualTo("foo"));
            Assert.That(result9.Then(Success9).UnwrapError(), Is.EqualTo("foo"));
            Assert.That(result10.Then(Success10).UnwrapError(), Is.EqualTo("foo"));
        });
    }
    
    [Test]
    public void Fail_All()
    {
        var result2 = Result.Success<Tuple2, string>((1, 2));
        var result3 = Result.Success<Tuple3, string>((1, 2, 3));
        var result4 = Result.Success<Tuple4, string>((1, 2, 3, 4));
        var result5 = Result.Success<Tuple5, string>((1, 2, 3, 4, 5));
        var result6 = Result.Success<Tuple6, string>((1, 2, 3, 4, 5, 6));
        var result7 = Result.Success<Tuple7, string>((1, 2, 3, 4, 5, 6, 7));
        var result8 = Result.Success<Tuple8, string>((1, 2, 3, 4, 5, 6, 7, 8));
        var result9 = Result.Success<Tuple9, string>((1, 2, 3, 4, 5, 6, 7, 8, 9));
        var result10 = Result.Success<Tuple10, string>((1, 2, 3, 4, 5, 6, 7, 8, 9, 10));
        Assert.Multiple(() =>
        {
            Assert.That(result2.Then(Failure2).UnwrapError(), Is.EqualTo("error"));
            Assert.That(result3.Then(Failure3).UnwrapError(), Is.EqualTo("error"));
            Assert.That(result4.Then(Failure4).UnwrapError(), Is.EqualTo("error"));
            Assert.That(result5.Then(Failure5).UnwrapError(), Is.EqualTo("error"));
            Assert.That(result6.Then(Failure6).UnwrapError(), Is.EqualTo("error"));
            Assert.That(result7.Then(Failure7).UnwrapError(), Is.EqualTo("error"));
            Assert.That(result8.Then(Failure8).UnwrapError(), Is.EqualTo("error"));
            Assert.That(result9.Then(Failure9).UnwrapError(), Is.EqualTo("error"));
            Assert.That(result10.Then(Failure10).UnwrapError(), Is.EqualTo("error"));
        });
    }
}