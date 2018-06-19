namespace System1Group.Lib.Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_RetainNotNull_Tests
    {
        [Test]
        public void Ok_True()
        {
            var result = new Success<object, string>(new { test = 6 });
            var newResult = result.RetainNotNull("error!");

            Assert.AreEqual(newResult.Unwrap(), result.Unwrap());
        }

        [Test]
        public void Ok_SquashFailure()
        {
            var error = "error!";
            var result = new Success<object, string>(null);
            var newResult = result.RetainNotNull(error);

            Assert.AreEqual(newResult.UnwrapError(), error);
        }
    }
}
