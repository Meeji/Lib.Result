namespace Result.Tests.AsyncResultTests;

using NUnit.Framework;

[TestFixture]
public class AsyncResult_MapToResultAsync_Tests
{
    [Test]
    public async Task Ok_Success()
    {
        var innerResult = Result.Success<int, string>(1);
        var asyncResult = innerResult.ToAsyncResult();

        var result = await asyncResult.ThenAsync(i => Result.Success<int, string>(i + 1)).UnwrapAsync();
        Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public async Task Ok_Failure()
    {
        var innerResult = Result.Failure<int, string>("Error");
        var asyncResult = innerResult.ToAsyncResult();

        var result = await asyncResult.ThenAsync(i => Result.Success<int, string>(i + 1)).UnwrapErrorAsync();
        Assert.That(result, Is.EqualTo("Error"));
    }
}
