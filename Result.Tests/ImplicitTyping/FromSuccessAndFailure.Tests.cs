namespace Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_FromSuccessAndFailure_Tests
    {
        [Test]
        public void Ok_Success()
        {
            var success = GetResult(true);

            Assert.IsInstanceOf<Success<int, string>>(success);
            Assert.AreEqual(success.Unwrap(), 5);
        }

        [Test]
        public void Ok_Failure()
        {
            var success = GetResult(false);

            Assert.IsInstanceOf<Failure<int, string>>(success);
            Assert.AreEqual(success.UnwrapError(), "Fail!");
        }

        private static Result<int, string> GetResult(bool success)
        {
            if (success)
            {
                return 5;
            }

            return "Fail!";
        }
    }
}
