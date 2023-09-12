namespace Result.Tests;

using System;
using System.Globalization;
using NUnit.Framework;

[TestFixture]
public class OptionalResult_Map_Tests
{
    [Test]
    public void Success_Ok()
    {
        var success = new Success<int, string>(5);
        var result = success.Map(i => (i + 5).ToString(CultureInfo.InvariantCulture));

        Assert.That(result.Unwrap(), Is.EqualTo("10"));
    }

    [Test]
    public void Failure_Ok()
    {
        var errorMessage = "error message";
        var failure = new Failure<int, string>(errorMessage);
        var result = failure.Map(i => (i + 5).ToString(CultureInfo.InvariantCulture));

        Assert.That(result.UnwrapError(), Is.EqualTo(errorMessage));
    }

    [Test]
    public void TwoCalls_Success()
    {
        var success = new Success<int, string>(5);
        var result = success.Map<string, object>(i => (i + 5).ToString(CultureInfo.InvariantCulture), _ => throw new Exception("Shouldn't be hit!"));

        Assert.That(result.Unwrap(), Is.EqualTo("10"));
    }

    [Test]
    public void TwoCalls_Failure()
    {
        var failure = new Failure<string, int>(5);
        var result = failure.Map<object, string>(_ => throw new Exception("Shouldn't be hit!"), i => (i + 5).ToString(CultureInfo.InvariantCulture));

        Assert.That(result.UnwrapError(), Is.EqualTo("10"));
    }
}
