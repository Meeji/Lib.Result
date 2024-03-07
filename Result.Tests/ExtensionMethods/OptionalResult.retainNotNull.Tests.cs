using Result.Unsafe;

namespace Result.Tests;

using NUnit.Framework;

[TestFixture]
public class OptionalResult_RetainNotNull_Tests
{
    [Test]
    public void Ok_True()
    {
        var result = new Success<object?, string>(new { test = 6 });
        var newResult = result.RetainNotNull("error!");

        Assert.That(result.Unwrap(), Is.EqualTo(newResult.Unwrap()));
    }

    [Test]
    public void Ok_Null()
    {
        var error = "error!";
        var result = new Success<object?, string>(null);
        var newResult = result.RetainNotNull(error);

        Assert.That(error, Is.EqualTo(newResult.UnwrapError()));
    }
}
