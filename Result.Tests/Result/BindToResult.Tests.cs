namespace Result.Tests;

using System;
using NUnit.Framework;

[TestFixture]
public class OptionalResult_MapToResult_Tests
{
    private readonly Func<int, Result<string, string>> testFunc = i =>
        {
            if (i == 5)
            {
                return new Success<string, string>("Well done!");
            }

            return new Failure<string, string>("Oops");
        };

    [Test]
    public void Success_Ok()
    {
        var success = new Success<int, string>(5);
        var failure = new Success<int, string>(6);

        var successResult = success.MapToResult(this.testFunc);
        var failureResult = failure.MapToResult(this.testFunc);

        Assert.That(successResult.Unwrap(), Is.EqualTo("Well done!"));
        Assert.That(failureResult.UnwrapError(), Is.EqualTo("Oops"));
    }

    [Test]
    public void Failure_Ok()
    {
        var failure = new Failure<int, string>("Failure");
        var failureResult = failure.MapToResult(this.testFunc);

        Assert.That(failureResult.UnwrapError(), Is.EqualTo("Failure"));
    }
}
