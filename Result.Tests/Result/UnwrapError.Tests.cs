namespace Result.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_UnwrapError_Tests
    {
        [Test]
        public void Ok()
        {
            var error = "error";
            var failure = new Failure<object, string>(error);
            Assert.AreEqual(error, failure.UnwrapError());
        }

        [Test]
        public void Unwrap_Success_DefaultError()
        {
            var obj = new { field = "field" };
            var success = new Success<object, string>(obj);
            var e = Assert.Throws<InvalidOperationException>(() => success.UnwrapError());
            Assert.AreEqual("Tried to unwrap error of a Success value", e.Message);
        }

        [Test]
        public void Unwrap_Success_CustomError()
        {
            var obj = new { field = "field" };
            var success = new Success<object, string>(obj);
            var e = Assert.Throws<InvalidOperationException>(() => success.UnwrapError("Error text"));
            Assert.AreEqual("Error text", e.Message);
        }
    }
}
