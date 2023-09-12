namespace System1Group.Lib.Result.Tests.AsyncResult
{
    using System;
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    public class AsyncResult_Create_Tests
    {
        [Test]
        public async Task FromResult()
        {
            var result = Result.Success<int, string>(12);
            var async = Lib.Result.AsyncResult.Create(result);
            Assert.That(result.Unwrap(), Is.EqualTo(await async.UnwrapAsync()));
        }

        [Test]
        public async Task FromFunc()
        {
            var result = Result.Success<int, string>(12);
            var func = (Func<Result<int, string>>)(() => result);
            var async = Lib.Result.AsyncResult.Create(func);
            Assert.That(result.Unwrap(), Is.EqualTo(await async.UnwrapAsync()));
        }

        [Test]
        public async Task FromTask()
        {
            var result = Result.Success<int, string>(12);
            var task = Task.FromResult(result);
            var async = Lib.Result.AsyncResult.Create(task);
            Assert.That(result.Unwrap(), Is.EqualTo(await async.UnwrapAsync()));
        }
    }
}
