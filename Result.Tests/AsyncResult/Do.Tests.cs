namespace System1Group.Lib.Result.Tests.AsyncResult
{
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    public class AsyncResult_Do_Tests
    {
        [Test]
        public void Ok_Success()
        {
            var innerResult = Task.FromResult(Result.Success<int, string>(1));
            var asyncResult = innerResult.ToAsyncResult();

            var result = asyncResult.Do(i => i + 1, e => 0);
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Ok_Failure()
        {
            var innerResult = Task.FromResult(Result.Failure<string, int>(1));
            var asyncResult = innerResult.ToAsyncResult();

            var result = asyncResult.Do(i => 0, e => e + 1);
            Assert.That(result, Is.EqualTo(2));
        }
    }
}
