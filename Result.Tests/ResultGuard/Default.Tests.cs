namespace Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ResultGuard_Default_Tests
    {
        [Test]
        public void Ok_Success()
        {
            Result<int, string> wrappedResult = new Success<int, string>(10);
            var guard = new ResultGuard<int, string, int>(wrappedResult);
            var result = guard.Default(i => i * 2).Do();
            Assert.AreEqual(result, 20);
        }

        [Test]
        public void Ok_Failure()
        {
            Result<int, string> wrappedResult = new Failure<int, string>("Error");
            var result = new ResultGuard<int, string, string>(wrappedResult).Default(i => i + i).Do();
            Assert.AreEqual(result, "ErrorError");
        }
    }
}
