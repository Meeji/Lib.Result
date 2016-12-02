namespace Result.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_LazyResult_BindToResult_Tests
    {
        private readonly Func<int, Result<string, string>> testFunc = i =>
            {
                if (i == 5)
                {
                    return new Success<string, string>("Well done!");
                }

                return new Failure<string, string>("Oops");
            };

        [Test]
        public void Success_Ok()
        {
            var called = 0;
            Func<Result<int, string>> factory = () =>
            {
                called++;
                return new Success<int, string>(5);
            };

            var success = LazyResult.Create(factory);

            var successResult = success.BindToResult(this.testFunc);

            Assert.AreEqual(called, 0);
            Assert.That(successResult.Unwrap(), Is.EqualTo("Well done!"));
            Assert.AreEqual(called, 1);
        }

        [Test]
        public void Success_BindsToFailure_Ok()
        {
            var called = 0;
            Func<Result<int, string>> factory = () =>
            {
                called++;
                return new Success<int, string>(6);
            };

            var success = LazyResult.Create(factory);

            var successResult = success.BindToResult(this.testFunc);

            Assert.AreEqual(called, 0);
            Assert.That(successResult.UnwrapError(), Is.EqualTo("Oops"));
            Assert.AreEqual(called, 1);
        }

        [Test]
        public void Failure_Ok()
        {
            var called = 0;
            Func<Result<int, string>> factory = () =>
            {
                called++;
                return new Failure<int, string>("Failure");
            };

            var failure = LazyResult.Create(factory);
            var failureResult = failure.BindToResult(this.testFunc);

            Assert.AreEqual(called, 0);
            Assert.That(failureResult.UnwrapError(), Is.EqualTo("Failure"));
            Assert.AreEqual(called, 1);
        }
    }
}
