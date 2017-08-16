namespace System1Group.Lib.Result.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_TryResult_Tests
    {
        [Test]
        public void Result_Ok()
        {
            var calls = 0;

            var factory = new Func<int>(
                () =>
                    {
                        calls += 1;
                        return calls;
                    });

            var result = new TryResult<int>(factory);

            Assert.AreEqual(calls, 0);

            Assert.AreEqual(result.Unwrap(), 1);
            Assert.AreEqual(calls, 1);

            Assert.AreEqual(result.Unwrap(), 1);
            Assert.AreEqual(calls, 1);
        }

        [Test]
        public void Result_Throws()
        {
            var exception = new Exception();
            var calls = 0;

            var factory = new Func<int>(
                () =>
                    {
                        calls += 1;
                        throw exception;
                    });

            var result = new TryResult<int>(factory);

            Assert.AreEqual(calls, 0);

            Assert.AreEqual(result.UnwrapError(), exception);
            Assert.AreEqual(calls, 1);

            Assert.AreEqual(result.UnwrapError(), exception);
            Assert.AreEqual(calls, 1);
        }

        [Test]
        public void IsSuccess_Ok()
        {
            Assert.AreEqual(new TryResult<int>(() => 5).IsSuccess, true);
            Assert.AreEqual(new TryResult<int>(() => { throw new Exception(); }).IsSuccess, false);
        }
    }
}