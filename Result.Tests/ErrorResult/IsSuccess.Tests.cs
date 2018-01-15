namespace System1Group.Lib.Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ErrorResult_IsSuccess_Tests
    {
        [Test]
        public void Ok_Success()
        {
            var result = ErrorResult.NoError<string>();

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.IsFailure, Is.False);
        }

        [Test]
        public void Ok_Failure()
        {
            var result = ErrorResult.WithError("some_error");

            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.IsFailure, Is.True);
        }
    }
}
