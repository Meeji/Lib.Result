namespace Result.Tests;

using NUnit.Framework;

[TestFixture]
public class OptionalResult_TryGet_Tests
{
    [Test]
    public void Ok_GetSuccess_Of_Success()
    {
        var successValue = 5;
        var success = Result.Success<int, string>(successValue);
        var result = success.TryGetSuccess(out var value);

        Assert.That(result, Is.True);
        Assert.That(value, Is.EqualTo(successValue));
    }

    [Test]
    public void Ok_GetFailure_Of_Success()
    {
        var successValue = 5;
        var success = Result.Success<int, string>(successValue);
        var result = success.TryGetFailure(out var value);

        Assert.That(result, Is.False);
        Assert.That(value, Is.EqualTo(default(string)));
    }

    [Test]
    public void Ok_GetFailure_Of_Failure()
    {
        var failureValue = 5;
        var failure = Result.Failure<string, int>(failureValue);
        var result = failure.TryGetFailure(out var value);

        Assert.That(result, Is.True);
        Assert.That(value, Is.EqualTo(failureValue));
    }

    [Test]
    public void Ok_GetSuccess_Of_Failure()
    {
        var failureValue = 5;
        var failure = Result.Failure<string, int>(failureValue);
        var result = failure.TryGetSuccess(out var value);

        Assert.That(result, Is.False);
        Assert.That(value, Is.EqualTo(default(string)));
    }
}
