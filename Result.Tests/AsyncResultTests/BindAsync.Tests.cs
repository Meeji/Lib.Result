namespace Result.Tests.AsyncResultTests;

using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class AsyncResult_MapAsync_Tests
{
    [Test]
    public async Task Ok_Success()
    {
        var innerResult = Result.Success<int, string>(1);
        var asyncResult = innerResult.ToAsyncResult();

        var result = await asyncResult.MapAsync(i => i + 1).UnwrapAsync();
        Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public async Task Ok_TwoFuncs()
    {
        var innerResult = Result.Success<int, string>(1);
        var asyncResult = innerResult.ToAsyncResult();

        var result = await asyncResult.MapAsync(i => i + 1, f => 0).UnwrapAsync();
        Assert.That(result, Is.EqualTo(2));
    }
}
