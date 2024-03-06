using Result.Unsafe;

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
    public void Ok_True_ResultFunc()
    {
        var result = new Success<int, string>(9);
        var newResult = result.RetainIf(i => i > 6 
            ? Result.Success<bool, string>(true)
            : Result.Failure<bool, string>("error!"));

        Assert.That(result.Unwrap(), Is.EqualTo(newResult.Unwrap()));
    }

    [Test]
    public void Ok_SquashFailure()
    {
        const string error = "error!";
        var result = new Success<int, string>(9);
        var newResult = result.RetainIf(i => i < 6, error);

        Assert.That(newResult.UnwrapError(), Is.EqualTo(error));
    }
    
    [Test]
    public void Ok_SquashFailure_ResultFunc()
    {
        const string error = "error!";
        var result = new Success<int, string>(9);
        var newResult = result.RetainIf(i => i < 6 
            ? Result.Success<bool, string>(true)
            : Result.Failure<bool, string>(error));

        Assert.That(newResult.UnwrapError(), Is.EqualTo(error));
    }
}
