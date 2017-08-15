namespace System1Group.Core.Result.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_FromSuccessAndFailureFuncs_Tests
    {
        [Test]
        public void Ok_Success()
        {
            var success = GetResult(true);

            Assert.IsInstanceOf<LazySuccess<int, string>>(success);
            Assert.AreEqual(success.Unwrap(), 5);
        }

        [Test]
        public void Ok_Failure()
        {
            var success = GetResult(false);

            Assert.IsInstanceOf<LazyFailure<int, string>>(success);
            Assert.AreEqual(success.UnwrapError(), "Fail!");
        }

        private static Result<int, string> GetResult(bool success)
        {
            if (success)
            {
                return new Func<int>(HandleSuccess);
            }

            return new Func<string>(() => "Fail!");
        }

        private static int HandleSuccess()
        {
            return 5;
        }
    }
}
