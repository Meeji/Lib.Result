namespace System1Group.Lib.Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_Constructor_Tests
    {
        [Test]
        public void Success_Ok()
        {
            Assert.DoesNotThrow(() => new Success<object, string>(new object()));
        }

        [Test]
        public void Failure_Ok()
        {
            Assert.DoesNotThrow(() => new Failure<object, string>("Error message"));
        }

        [Test]
        public void Success_Ok_WithNull()
        {
            Assert.DoesNotThrow(() => new Success<object?, string>(null));
        }
    }
}
