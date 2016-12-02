namespace Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_ToBool_Tests
    {
        [Test]
        public void Ok()
        {
            var success = new Success<int, int>(5);
            var failure = new Failure<int, int>(5);

            if (!success)
            {
                Assert.Fail();
            }

            if (failure)
            {
                Assert.Fail();
            }

            Assert.Pass();
        }
    }
}
