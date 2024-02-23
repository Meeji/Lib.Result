namespace Result.Tests.AsyncResultTests;

using System.Threading.Tasks;
using Exceptions;
using NUnit.Framework;

[TestFixture]
public class AsyncResult_UnwrapErrorAsync_Tests
{
    [Test]
    public async Task Ok()
    {
        var error = "error";
        var failure = new Failure<object, string>(error);
        var async = failure.ToAsyncResult();
        Assert.That(await async.UnwrapErrorAsync(), Is.EqualTo(error));
    }

    [Test]
    public void Unwrap_Success_DefaultError()
    {
        var obj = new { field = "field" };
        var success = new Success<object, string>(obj);
        var async = success.ToAsyncResult();
        var e = Assert.ThrowsAsync<InvalidUnwrapException>(async () => await async.UnwrapErrorAsync());
        Assert.That(e?.Message, Does.StartWith("Tried to unwrap"));
        Assert.That(e?.Result, Is.EqualTo(async));
        Assert.That(e?.Item, Is.EqualTo(obj));
        Assert.That(e?.FailedUnwrapType, Is.EqualTo(InvalidUnwrapException.UnwrapType.Failure));
    }

    [Test]
    public void Unwrap_Success_CustomError()
    {
        var error = "Error text";
        var obj = new { field = "field" };
        var success = new Success<object, string>(obj);
        var async = success.ToAsyncResult();
        var e = Assert.ThrowsAsync<InvalidUnwrapException>(async () => await async.UnwrapErrorAsync(error));
        Assert.That(e?.Message, Is.EqualTo(error));
        Assert.That(e?.Result, Is.EqualTo(async));
        Assert.That(e?.Item, Is.EqualTo(obj));
        Assert.That(e?.FailedUnwrapType, Is.EqualTo(InvalidUnwrapException.UnwrapType.Failure));
    }
}
