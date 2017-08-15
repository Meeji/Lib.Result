namespace System1Group.Core.Result.Tests
{
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_CombineToResult_Tests
    {
        [Test]
        public void Success_Ok()
        {
            var helloWorld = new Success<string, string>("Hello world");
            var letters = new Success<int, string>(3);

            var truncated = helloWorld.Combine(letters, (s, i) => new string(s.Take(i).ToArray()));

            Assert.That(truncated.Unwrap(), Is.EqualTo("Hel"));

            var error = helloWorld.CombineToResult(letters, (s, i) => new Failure<object, string>("Error"));

            Assert.That(truncated.Unwrap(), Is.EqualTo("Hel"));
            Assert.That(error.UnwrapError(), Is.EqualTo("Error"));
        }

        [Test]
        public void Failure_Ok()
        {
            var success = new Success<int, string>(5);
            var failure = new Failure<int, string>("Failure");

            var result1 = success.CombineToResult(failure, (s, f) => new Success<int, string>(s + f));
            var result2 = failure.CombineToResult(success, (f, s) => new Success<int, string>(s + f));

            Assert.That(result1.UnwrapError(), Is.EqualTo("Failure"));
            Assert.That(result2.UnwrapError(), Is.EqualTo("Failure"));
        }
    }
}
