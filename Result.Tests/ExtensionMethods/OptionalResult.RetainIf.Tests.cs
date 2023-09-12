namespace Result.Tests;

using NUnit.Framework;

[TestFixture]
public class OptionalResult_RetainIf_Tests
{
    [Test]
    public void Ok_True()
    {
        var result = new Success<int, string>(9);
        var newResult = result.RetainIf(i => i > 6, "error!");

        Assert.That(result.Unwrap(), Is.EqualTo(newResult.Unwrap()));
    }

    [Test]
    public void Ok_SquashFailure()
    {
        var error = "error!";
        var result = new Success<int, string>(9);
        var newResult = result.RetainIf(i => i < 6, error);

        Assert.That(error, Is.EqualTo(newResult.UnwrapError()));
    }
}
