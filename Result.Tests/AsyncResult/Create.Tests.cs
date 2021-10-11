namespace System1Group.Lib.Result.Tests.AsyncResult
{
    using System;
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    public class AsyncResult_Create_Tests
    {
        [Test]
        public void FromResult()
        {
            var result = Result.Success<int, string>(12);
            var async = Lib.Result.AsyncResult.Create(result);
            Assert.That(result.Unwrap(), Is.EqualTo(async.Unwrap()));
        }

        [Test]
        public void FromFunc()
        {
            var result = Result.Success<int, string>(12);
            var func = (Func<Result<int, string>>)(() => result);
            var async = Lib.Result.AsyncResult.Create(func);
            Assert.That(result.Unwrap(), Is.EqualTo(async.Unwrap()));
        }

        [Test]
        public void FromTask()
        {
            var result = Result.Success<int, string>(12);
            var task = Task.FromResult(result);
            var async = Lib.Result.AsyncResult.Create(task);
            Assert.That(result.Unwrap(), Is.EqualTo(async.Unwrap()));
        }
    }
}
