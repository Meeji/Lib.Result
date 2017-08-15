namespace System1Group.Core.Result.Tests
{
    using System;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_LazyResult_Combine_Tests
    {
        [Test]
        public void Success_Ok()
        {
            var called = 0;
            Func<Result<string, string>> factory = () =>
            {
                called++;
                return new Success<string, string>("Hello world");
            };

            var helloWorld = LazyResult.Create(factory);
            var letters = new Success<int, string>(3);

            var truncated = helloWorld.Combine(letters, (s, i) => new string(s.Take(i).ToArray()));

            Assert.AreEqual(called, 0);
            Assert.That(truncated.Unwrap(), Is.EqualTo("Hel"));
            Assert.AreEqual(called, 1);
        }

        [Test]
        public void Failure_Ok()
        {
            var calledSuccess = 0;
            Func<Result<int, string>> successFactory = () =>
            {
                calledSuccess++;
                return new Success<int, string>(5);
            };

            var calledFailure = 0;
            Func<Result<int, string>> failureFactory = () =>
            {
                calledFailure++;
                return new Failure<int, string>("Failure");
            };

            var success = LazyResult.Create(successFactory);
            var failure = LazyResult.Create(failureFactory);

            var result1 = success.Combine(failure, (s, f) => s + f);
            var result2 = failure.Combine(success, (f, s) => s + f);

            Assert.AreEqual(calledFailure, 0);
            Assert.AreEqual(calledSuccess, 0);
            Assert.That(result1.UnwrapError(), Is.EqualTo("Failure"));
            Assert.That(result2.UnwrapError(), Is.EqualTo("Failure"));
            Assert.AreEqual(calledFailure, 1);
            Assert.AreEqual(calledSuccess, 1);
        }
    }
}
