namespace System1Group.Lib.Result.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_LazyResult_Tests
    {
        [Test]
        public void Result_Ok()
        {
            var calls = 0;

            var factory = new Func<Result<int, object>>(() =>
                {
                    calls += 1;
                    return new Success<int, object>(calls);
                });

            var result = LazyResult.Create(factory);

            Assert.AreEqual(calls, 0);

            Assert.AreEqual(result.Unwrap(), 1);
            Assert.AreEqual(calls, 1);

            Assert.AreEqual(result.Unwrap(), 1);
            Assert.AreEqual(calls, 1);
        }

        [Test]
        public void IsSuccess_Ok()
        {
            Assert.AreEqual(LazyResult.Create(() => new Success<int, object>(4)).IsSuccess, true);
            Assert.AreEqual(LazyResult.Create(() => new Failure<object, int>(4)).IsSuccess, false);
        }
    }
}