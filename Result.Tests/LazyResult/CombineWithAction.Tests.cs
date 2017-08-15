namespace System1Group.Core.Result.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_LazyResult_CombineWithAction_Tests
    {
        [Test]
        public void Success_Ok()
        {
            var called = 0;
            Func<Result<List<int>, string>> factory = () =>
            {
                called++;
                return new Success<List<int>, string>(new List<int> { 1, 2, 3 });
            };

            var baseList = LazyResult.Create(factory);
            var addInt = new Success<int, string>(4);

#pragma warning disable CS0618 // Type or member is obsolete
            var result = baseList.Combine(addInt, (list, i) => list.Add(i));
#pragma warning restore CS0618 // Type or member is obsolete

            Assert.AreEqual(called, 0);
            result.Unwrap(); // This is why I've obsoleted this Combine using action method - you wouldn't expect to have to use this line to get the correct value out of baseList
            CollectionAssert.AreEquivalent(baseList.Unwrap(), new[] { 1, 2, 3, 4 });
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
