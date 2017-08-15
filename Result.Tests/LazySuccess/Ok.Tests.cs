namespace System1Group.Core.Result.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_LazySuccess_Tests
    {
        [Test]
        public void Success_Ok()
        {
            var calls = 0;

            var factory = new Func<int>(() =>
                {
                    calls += 1;
                    return calls;
                });

            var result = new LazySuccess<int, object>(factory);

            Assert.AreEqual(calls, 0);

            Assert.AreEqual(result.Unwrap(), 1);
            Assert.AreEqual(calls, 1);

            Assert.AreEqual(result.Unwrap(), 1);
            Assert.AreEqual(calls, 1);
        }

        [Test]
        public void IsSuccess_Ok()
        {
            Assert.AreEqual(new LazySuccess<int, object>(() => 5).IsSuccess, true);
        }
    }
}