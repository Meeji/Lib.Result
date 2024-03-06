using Result.Unsafe;

namespace Result.Tests.AsyncResultTests;

using System;
using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class AsyncResult_ToAsyncResult_Tests
{
    [Test]
    public async Task FromResult()
    {
        var result = Result.Success<int, string>(12);
        var async = result.ToAsyncResult();
        Assert.That(result.Unwrap(), Is.EqualTo(await async.UnwrapAsync()));
    }

    [Test]
    public async Task FromFunc()
    {
        var result = Result.Success<int, string>(12);
        var func = (Func<Result<int, string>>)(() => result);
        var async = func.ToAsyncResult();
        Assert.That(result.Unwrap(), Is.EqualTo(await async.UnwrapAsync()));
    }

    [Test]
    public async Task FromTask()
    {
        var result = Result.Success<int, string>(12);
        var task = Task.FromResult(result);
        var async = task.ToAsyncResult();
        Assert.That(result.Unwrap(), Is.EqualTo(await async.UnwrapAsync()));
    }
}
