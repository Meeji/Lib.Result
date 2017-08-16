namespace System1Group.Lib.Result.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_Unwrap_Tests
    {
        [Test]
        public void Success_Ok()
        {
            var obj = new { field = "field" };
            var success = new Success<object, string>(obj);
            var obj2 = success.Unwrap();

            Assert.That(obj, Is.EqualTo(obj2));
        }

        [Test]
        public void Failure_Ok()
        {
            var error = "error message";
            var error2 = "second error message";
            var failure = new Failure<object, string>(error);

            var e = Assert.Throws<InvalidOperationException>(() => failure.Unwrap(error2));
            Assert.That(e.Message, Is.EqualTo(error2));
        }

        [Test]
        public void Failure_Ok_WithoutMessage()
        {
            var error = "error message";
            var failure = new Failure<object, string>(error);

            var e = Assert.Throws<InvalidOperationException>(() => failure.Unwrap());
            Assert.That(e.Message, Is.EqualTo(error));
        }
    }
}
