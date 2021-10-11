namespace System1Group.Lib.Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ErrorResult_Implicitconversion_Tests
    {
        [Test]
        public void Ok()
        {
            var error = "error";
            ErrorResult<string> ErrorCreator() => error;

            var result = ErrorCreator();

            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.UnwrapError(), Is.EqualTo(error));
        }
    }
}
