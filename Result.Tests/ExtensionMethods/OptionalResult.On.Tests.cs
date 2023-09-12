namespace Result.Tests;

using NUnit.Framework;

[TestFixture]
public class OptionalResult_On_Tests
{
    [Test]
    public void Ok_Success()
    {
        var success = false;
        var successValue = 0;

        var failure = false;
        var failureValue = string.Empty;
        var result = new Success<int, string>(9);

        result.OnFailure(err =>
        {
            failure = true;
            failureValue = err;
        });

        result.OnSuccess(suc =>
        {
            success = true;
            successValue = suc;
        });

        Assert.That(success, Is.True);
        Assert.That(failure, Is.False);

        Assert.That(failureValue, Is.EqualTo(string.Empty));
        Assert.That(successValue, Is.EqualTo(9));
    }

    [Test]
    public void Ok_Failure()
    {
        var success = false;
        var successValue = 0;

        var failure = false;
        var failureValue = string.Empty;
        var result = new Failure<int, string>("error!");

        result.OnFailure(err =>
        {
            failure = true;
            failureValue = err;
        });

        result.OnSuccess(suc =>
        {
            success = true;
            successValue = suc;
        });

        Assert.That(success, Is.False);
        Assert.That(failure, Is.True);

        Assert.That(failureValue, Is.EqualTo("error!"));
        Assert.That(successValue, Is.EqualTo(0));
    }
}
