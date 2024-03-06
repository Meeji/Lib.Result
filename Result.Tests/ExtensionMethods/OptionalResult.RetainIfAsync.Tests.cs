using Result.Unsafe;

namespace Result.Tests;

using NUnit.Framework;

[TestFixture]
public class OptionalResult_RetainIfAsync_Tests
{
    [Test]
    public async Task Ok_True_ResultFunc()
    {
        var result = new Success<int, string>(9);
        var newResult = await result.RetainIfAsync(i => i > 6 
            ? Task.FromResult(Result.Success<bool, string>(true))
            : Task.FromResult(Result.Failure<bool, string>("error!")));

        Assert.That(result.Unwrap(), Is.EqualTo(newResult.Unwrap()));
    }
    
    [Test]
    public async Task Ok_SquashFailure_ResultFunc()
    {
        const string error = "error!";
        var result = new Success<int, string>(9);
        var newResult = await result.RetainIfAsync(i => i < 6 
            ? Task.FromResult(Result.Success<bool, string>(true))
            : Task.FromResult(Result.Failure<bool, string>(error)));

        Assert.That(newResult.UnwrapError(), Is.EqualTo(error));
    }
}
