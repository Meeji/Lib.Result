namespace System1Group.Lib.Result.Tests.AsyncResult
{
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    public class AsyncResult_DoAsync_Tests
    {
        [Test]
        public async Task Ok_Success()
        {
            var innerResult = Task.FromResult(Result.Success<int, string>(1));
            var asyncResult = innerResult.ToAsyncResult();

            var result = await asyncResult.DoAsync(i => i + 1, e => 0);
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public async Task Ok_Failure()
        {
            var innerResult = Task.FromResult(Result.Failure<string, int>(1));
            var asyncResult = innerResult.ToAsyncResult();

            var result = await asyncResult.DoAsync(i => 0, e => e + 1);
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public async Task Ok_Success_Action()
        {
            var innerResult = Task.FromResult(Result.Success<int, string>(1));
            var asyncResult = innerResult.ToAsyncResult();
            var result = -1;
            await asyncResult.DoAsync(i => { result = i + 1; }, e => { result = 0; });
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public async Task Ok_Failure_Action()
        {
            var innerResult = Task.FromResult(Result.Failure<string, int>(1));
            var asyncResult = innerResult.ToAsyncResult();
            var result = -1;
            await asyncResult.DoAsync(i => { result = 0; }, e => { result = e + 1; });
            Assert.That(result, Is.EqualTo(2));
        }
    }
}
