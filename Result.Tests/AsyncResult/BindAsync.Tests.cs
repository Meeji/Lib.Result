namespace System1Group.Lib.Result.Tests.AsyncResult
{
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    public class AsyncResult_BindAsync_Tests
    {
        [Test]
        public void Ok_Success()
        {
            var innerResult = Result.Success<int, string>(1);
            var asyncResult = innerResult.ToAsyncResult();

            var result = asyncResult.BindAsync(i => i + 1).Unwrap();
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Ok_TwoFuncs()
        {
            var innerResult = Result.Success<int, string>(1);
            var asyncResult = innerResult.ToAsyncResult();

            var result = asyncResult.BindAsync(i => i + 1, f => 0).Unwrap();
            Assert.That(result, Is.EqualTo(2));
        }
    }
}
