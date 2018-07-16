namespace System1Group.Lib.Result.Tests.AsyncResult
{
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    public class AsyncResult_ToTask_Tests
    {
        [Test]
        public async Task Ok()
        {
            var result = Result.Success<int, string>(12);
            var async = result.ToAsyncResult();
            var task = async.ToTask();
            Assert.That(await task, Is.SameAs(result));
        }
    }
}
