namespace System1Group.Lib.Result.Tests.AsyncResult
{
    using System.Threading;
    using NUnit.Framework;

    [TestFixture]
    public class AsyncResult_BindToResultAsync_Tests
    {
        [Test]
        public void Ok_Success()
        {
            var innerResult = Result.Success<int, string>(1);
            var asyncResult = innerResult.ToAsyncResult();

            var result = asyncResult.BindToResultAsync(i => Result.Success<int, string>(i + 1)).Unwrap();
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Ok_Failure()
        {
            var innerResult = Result.Failure<int, string>("Error");
            var asyncResult = innerResult.ToAsyncResult();

            var result = asyncResult.BindToResultAsync(i => Result.Success<int, string>(i + 1)).UnwrapError();
            Assert.That(result, Is.EqualTo("Error"));
        }
    }
}
