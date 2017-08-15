namespace System1Group.Core.Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ResultGuard_Where_Tests
    {
        [Test]
        public void Ok_Success()
        {
            Result<int, string> wrappedResult = new Success<int, string>(10);
            var guard = new ResultGuard<int, string, int>(wrappedResult);
            guard.Where(i => i == 5, i => 0);
            guard.Where(i => i == 10, i => i * 2);
            var result = guard.Do();
            Assert.AreEqual(result, 20);
        }

        [Test]
        public void Ok_Failure()
        {
            Result<int, string> wrappedResult = new Failure<int, string>("Error");
            var guard = new ResultGuard<int, string, string>(wrappedResult);
            guard.Where(i => i == "something", i => string.Empty);
            guard.Where(i => i == "Error", i => i + i);
            var result = guard.Do();
            Assert.AreEqual(result, "ErrorError");
        }

        [Test]
        public void Ok_Success_MultipleMatches()
        {
            Result<int, string> wrappedResult = new Success<int, string>(10);
            var guard = new ResultGuard<int, string, string>(wrappedResult);
            guard.Where(i => i == 10, i => "first match");
            guard.Where(i => i == 10, i => "second match");
            var result = guard.Do();
            Assert.AreEqual(result, "first match");
        }

        [Test]
        public void Ok_Failure_MultipleMatches()
        {
            Result<int, string> wrappedResult = new Failure<int, string>("Error");
            var guard = new ResultGuard<int, string, string>(wrappedResult);
            guard.Where(i => i == "Error", i => "first match");
            guard.Where(i => i == "Error", i => "second match");
            var result = guard.Do();
            Assert.AreEqual(result, "first match");
        }

        [Test]
        public void Ok_Success_FallsThroughToDefault()
        {
            Result<int, string> wrappedResult = new Success<int, string>(10);
            var guard = new ResultGuard<int, string, string>(wrappedResult);
            guard.Where(i => i == 11, i => "no match");
            guard.Where(i => i == 11, i => "no match");
            guard.Default((int i) => "match");
            var result = guard.Do();
            Assert.AreEqual(result, "match");
        }

        [Test]
        public void Ok_Failure_FallsThroughToDefault()
        {
            Result<int, string> wrappedResult = new Failure<int, string>("Error");
            var guard = new ResultGuard<int, string, string>(wrappedResult);
            guard.Where(i => i == string.Empty, i => "no match");
            guard.Where(i => i == string.Empty, i => "no match");
            guard.Default((string i) => "match");
            var result = guard.Do();
            Assert.AreEqual(result, "match");
        }
    }
}
