namespace System1Group.Core.Result.Tests
{
    using System.Globalization;
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_Bind_Tests
    {
        [Test]
        public void Success_Ok()
        {
            var success = new Success<int, string>(5);
            var result = success.Bind(i => (i + 5).ToString(CultureInfo.InvariantCulture));

            Assert.That(result.Unwrap(), Is.EqualTo("10"));
        }

        [Test]
        public void Failure_Ok()
        {
            var errorMessage = "error message";
            var success = new Failure<int, string>(errorMessage);
            var result = success.Bind(i => (i + 5).ToString(CultureInfo.InvariantCulture));

            Assert.That(result.UnwrapError(), Is.EqualTo(errorMessage));
        }
    }
}
