namespace Result.Tests;

using System;
using NUnit.Framework;

[TestFixture]
public class OptionalResult_UnwrapOrThrow_Tests
{
    [Test]
    public void Ok_NormalExcetion()
    {
        var exception = new Exception();
        Result<int, Exception> result = new Failure<int, Exception>(exception);

        var exc = Assert.Throws<Exception>(() => result.UnwrapOrThrow());

        Assert.That(exception, Is.EqualTo(exc));
    }

    [Test]
    public void Ok_DerivedExcetion()
    {
        var exception = new AccessViolationException();
        Result<int, AccessViolationException> result = new Failure<int, AccessViolationException>(exception);

        var exc = Assert.Throws<AccessViolationException>(() => result.UnwrapOrThrow());

        Assert.That(exception, Is.EqualTo(exc));
    }

    [Test]
    public void Ok_UnwrapsGoodValue()
    {
        Result<int, Exception> result = new Success<int, Exception>(5);

        Assert.That(result.UnwrapOrThrow(), Is.EqualTo(5));
    }
}
