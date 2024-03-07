using Result.Unsafe;

namespace Result.Tests;

using NUnit.Framework;

[TestFixture]
public class OptionalResult_SuccessAndFailure_Tests
{
    [Test]
    public void Success_Ok()
    {
        var wrapped = "s";
        var success = Result.Success<string, int>(wrapped);

        Assert.That(success.Unwrap(), Is.EqualTo(wrapped));
    }

    [Test]
    public void Failure_Ok()
    {
        var wrapped = 5;
        var success = Result.Failure<string, int>(wrapped);

        Assert.That(success.UnwrapError(), Is.EqualTo(wrapped));
    }
}
