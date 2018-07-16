namespace System1Group.Lib.Result.Tests.AsyncResult
{
    using System;
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    public class AsyncResult_ToAsyncResult_Tests
    {
        [Test]
        public void FromResult()
        {
            var result = Result.Success<int, string>(12);
            var async = result.ToAsyncResult();
            Assert.That(result.Unwrap(), Is.EqualTo(async.Unwrap()));
        }

        [Test]
        public void FromFunc()
        {
            var result = Result.Success<int, string>(12);
            var func = (Func<Result<int, string>>)(() => result);
            var async = func.ToAsyncResult();
            Assert.That(result.Unwrap(), Is.EqualTo(async.Unwrap()));
        }

        [Test]
        public void FromTask()
        {
            var result = Result.Success<int, string>(12);
            var task = Task.FromResult(result);
            var async = task.ToAsyncResult();
            Assert.That(result.Unwrap(), Is.EqualTo(async.Unwrap()));
        }
    }
}
