namespace System1Group.Core.Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_Properties_Tests
    {
        [Test]
        public void Success_Ok()
        {
            var result = new Success<object, string>(new object());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.IsFailure, Is.False);
        }

        [Test]
        public void Failure_Ok()
        {
            var result = new Failure<object, string>("Error message");
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.IsFailure, Is.True);
        }
    }
}
