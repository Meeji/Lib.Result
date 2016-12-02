namespace Result.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_ToSuccess_Tests
    {
        [Test]
        public void Ok_Success()
        {
            var success = new Success<int, string>(5);
            int unwrappedSuccess = success;
            Assert.AreEqual(unwrappedSuccess, 5);
        }

        [Test]
        public void UnwrapFailure()
        {
            var failure = new Failure<int, string>("Fail!");
            int unwrappedFailure;
            Assert.Throws<InvalidOperationException>(() => unwrappedFailure = failure);
        }
    }
}
