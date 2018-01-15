namespace System1Group.Lib.Result.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_LazyResult_CombineWithAction_Tests
    {
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

            Assert.AreEqual(calledFailure, 0);
            Assert.AreEqual(calledSuccess, 0);
            var result1 = success.Combine(failure, (s, f) => s + f);
            var result2 = failure.Combine(success, (f, s) => s + f);

            Assert.That(result1.UnwrapError(), Is.EqualTo("Failure"));
            Assert.That(result2.UnwrapError(), Is.EqualTo("Failure"));

            Assert.AreEqual(calledFailure, 1);
            Assert.AreEqual(calledSuccess, 1);
        }
    }
}
