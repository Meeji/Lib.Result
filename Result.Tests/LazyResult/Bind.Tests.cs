namespace System1Group.Lib.Result.Tests
{
    using System;
    using System.Globalization;
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_LazyResult_Bind_Tests
    {
        [Test]
        public void Success_Ok()
        {
            var called = 0;
            Func<Result<int, string>> factory = () =>
                {
                    called++;
                    return new Success<int, string>(5);
                };

            var result = LazyResult.Create(factory).Bind(i => (i + 5).ToString(CultureInfo.InvariantCulture));

            Assert.AreEqual(called, 0);
            Assert.That(result.Unwrap(), Is.EqualTo("10"));
            Assert.AreEqual(called, 1);
        }

        [Test]
        public void Failure_Ok()
        {
            var called = 0;
            var errorMessage = "Error!";
            Func<Result<int, string>> factory = () =>
            {
                called++;
                return new Failure<int, string>(errorMessage);
            };

            var result = LazyResult.Create(factory).Bind(i => (i + 5).ToString(CultureInfo.InvariantCulture));

            Assert.AreEqual(called, 0);
            Assert.That(result.UnwrapError(), Is.EqualTo(errorMessage));
            Assert.AreEqual(called, 1);
        }

        [Test]
        public void TwoCalls_Success_Ok()
        {
            var called = 0;
            Func<Result<int, string>> factory = () =>
            {
                called++;
                return new Success<int, string>(5);
            };

            var result = LazyResult.Create(factory)
                .Bind<string, object>(i => (i + 5).ToString(CultureInfo.InvariantCulture), _ => throw new Exception("Should not hit this!"));

            Assert.AreEqual(called, 0);
            Assert.That(result.Unwrap(), Is.EqualTo("10"));
            Assert.AreEqual(called, 1);
        }

        [Test]
        public void TwoCalls_Failure_Ok()
        {
            var called = 0;
            Func<Result<string, int>> factory = () =>
            {
                called++;
                return new Failure<string, int>(5);
            };

            var result = LazyResult.Create(factory)
                .Bind<object, string>(_ => throw new Exception("Should not hit this!"), i => (i + 5).ToString(CultureInfo.InvariantCulture));

            Assert.AreEqual(called, 0);
            Assert.That(result.UnwrapError(), Is.EqualTo("10"));
            Assert.AreEqual(called, 1);
        }
    }
}
