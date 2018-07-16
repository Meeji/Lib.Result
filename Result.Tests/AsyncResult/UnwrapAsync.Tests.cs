namespace System1Group.Lib.Result.Tests.AsyncResult
{
    using System.Threading.Tasks;
    using Exceptions;
    using NUnit.Framework;

    [TestFixture]
    public class AsyncResult_UnwrapAsync_Tests
    {
        [Test]
        public async Task Success_Ok()
        {
            var obj = new { field = "field" };
            var success = new Success<object, string>(obj);
            var async = success.ToAsyncResult();
            var obj2 = await async.UnwrapAsync();

            Assert.That(obj, Is.EqualTo(obj2));
        }

        [Test]
        public void Failure_Ok()
        {
            var error = "error message";
            var error2 = "second error message";
            var failure = new Failure<object, string>(error);
            var async = failure.ToAsyncResult();

            var e = Assert.ThrowsAsync<InvalidUnwrapException>(async () => await async.UnwrapAsync(error2));
            Assert.That(e.Message, Is.EqualTo(error2));
            Assert.That(e.Result, Is.EqualTo(async));
            Assert.That(e.Item, Is.EqualTo(error));
            Assert.That(e.FailedUnwrapType, Is.EqualTo(InvalidUnwrapException.UnwrapType.Success));
        }

        [Test]
        public void Failure_Ok_WithoutMessage()
        {
            var error = "error message";
            var failure = new Failure<object, string>(error);
            var async = failure.ToAsyncResult();

            var e = Assert.ThrowsAsync<InvalidUnwrapException>(async () => await async.UnwrapAsync());
            Assert.That(e.Message, Does.StartWith("Tried to unwrap"));
            Assert.That(e.Result, Is.EqualTo(async));
            Assert.That(e.Item, Is.EqualTo(error));
            Assert.That(e.FailedUnwrapType, Is.EqualTo(InvalidUnwrapException.UnwrapType.Success));
        }
    }
}
