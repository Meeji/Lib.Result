namespace System1Group.Lib.Result.Tests
{
    using System;
    using Exceptions;
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
            var e = Assert.Throws<InvalidUnwrapException>(() => success.UnwrapError());
            Assert.That(e.Message, Does.StartWith("Tried to unwrap"));
            Assert.That(e.Result, Is.EqualTo(success));
            Assert.That(e.Item, Is.EqualTo(obj));
            Assert.That(e.FailedUnwrapType, Is.EqualTo(InvalidUnwrapException.UnwrapType.Failure));
        }

        [Test]
        public void Unwrap_Success_CustomError()
        {
            var error = "Error text";
            var obj = new { field = "field" };
            var success = new Success<object, string>(obj);
            var e = Assert.Throws<InvalidUnwrapException>(() => success.UnwrapError(error));
            Assert.That(e.Message, Is.EqualTo(error));
            Assert.That(e.Result, Is.EqualTo(success));
            Assert.That(e.Item, Is.EqualTo(obj));
            Assert.That(e.FailedUnwrapType, Is.EqualTo(InvalidUnwrapException.UnwrapType.Failure));
        }
    }
}
