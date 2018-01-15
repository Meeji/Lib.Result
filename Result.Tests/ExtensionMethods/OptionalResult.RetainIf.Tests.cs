namespace System1Group.Lib.Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_RetainIf_Tests
    {
        [Test]
        public void Ok_True()
        {
            var result = new Success<int, string>(9);
            var newResult = result.RetainIf(i => i > 6, "error!");

            Assert.AreEqual(newResult.Unwrap(), result.Unwrap());
        }

        [Test]
        public void Ok_SquashFailure()
        {
            var error = "error!";
            var result = new Success<int, string>(9);
            var newResult = result.RetainIf(i => i < 6, error);

            Assert.AreEqual(newResult.UnwrapError(), error);
        }
    }
}
