namespace System1Group.Lib.Result.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_LazyFailure_Tests
    {
        [Test]
        public void Failure_Ok()
        {
            var calls = 0;

            var factory = new Func<int>(() =>
                {
                    calls += 1;
                    return calls;
                });

            var result = new LazyFailure<object, int>(factory);

            Assert.AreEqual(calls, 0);

            Assert.AreEqual(result.UnwrapError(), 1);
            Assert.AreEqual(calls, 1);

            Assert.AreEqual(result.UnwrapError(), 1);
            Assert.AreEqual(calls, 1);
        }

        [Test]
        public void IsSuccess_Ok()
        {
            Assert.AreEqual(new LazyFailure<object, int>(() => 5).IsSuccess, false);
        }
    }
}