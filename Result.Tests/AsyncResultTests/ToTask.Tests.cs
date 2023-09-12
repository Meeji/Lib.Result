namespace Result.Tests.AsyncResultTests;

using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class AsyncResult_ToTaskAsync_Tests
{
    [Test]
    public async Task Ok()
    {
        var result = Result.Success<int, string>(12);
        var async = result.ToAsyncResult();
        var task = async.ToTaskAsync();
        Assert.That(await task, Is.SameAs(result));
    }
}
