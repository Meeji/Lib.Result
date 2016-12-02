namespace Result.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_Do_Func_Tests
    {
        private const string Error = "error";

        private static readonly object Obj = new object();

        private readonly Result<object, string> failure = new Failure<object, string>(Error);

        private readonly Result<object, string> success = new Success<object, string>(Obj);
        
        [Test]
        public void Success_Ok()
        {
            var result = this.success.Do(
                onSuccess: o => o,
                onFailure: err => new object());

            Assert.That(Obj, Is.EqualTo(result));
        }

        [Test]
        public void Failure_Ok()
        {
            var result = this.failure.Do(
                onSuccess: o => null,
                onFailure: err => err);

            Assert.That(Error, Is.EqualTo(result));
        }

        [Test]
        public void Success_NullFunc_WillThrow()
        {
            Assert.Throws<ArgumentNullException>(() => this.success.Do<int>(
                onSuccess: null,
                onFailure: null));
        }

        [Test]
        public void Failure_NullFunc_WillThrow()
        {
            Assert.Throws<ArgumentNullException>(() => this.failure.Do<int>(
                onSuccess: null,
                onFailure: null));
        }
    }
}