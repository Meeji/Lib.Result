namespace System1Group.Lib.Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_ChangeFailure_Tests
    {
        [Test]
        public void Ok_Success_ValueChange()
        {
            var wrapped = "s";
            var result = Result.Success<string, int>(wrapped).ChangeFailure(true);

            Assert.That(result, Is.AssignableTo<Result<string, bool>>());
            Assert.That(result.Unwrap(), Is.EqualTo(wrapped));
        }

        [Test]
        public void Ok_Failure_ValueChange()
        {
            var result = Result.Failure<string, int>(3).ChangeFailure(true);

            Assert.That(result, Is.AssignableTo<Result<string, bool>>());
            Assert.That(result.UnwrapError(), Is.EqualTo(true));
        }

        [Test]
        public void Ok_Success_FuncChange()
        {
            var wrapped = "s";
            var result = Result.Success<string, int>(wrapped).ChangeFailure(() => true);

            Assert.That(result, Is.AssignableTo<Result<string, bool>>());
            Assert.That(result.Unwrap(), Is.EqualTo(wrapped));
        }

        [Test]
        public void Ok_Failure_FuncChange()
        {
            var result = Result.Failure<string, int>(3).ChangeFailure(() => true);

            Assert.That(result, Is.AssignableTo<Result<string, bool>>());
            Assert.That(result.UnwrapError(), Is.EqualTo(true));
        }

        [Test]
        public void Ok_Success_FuncT2Change()
        {
            var wrapped = "s";
            var result = Result.Success<string, int>(wrapped).ChangeFailure(e => e == 2);

            Assert.That(result, Is.AssignableTo<Result<string, bool>>());
            Assert.That(result.Unwrap(), Is.EqualTo(wrapped));
        }

        [Test]
        public void Ok_Failure_FuncT2Change()
        {
            var result = Result.Failure<string, int>(3).ChangeFailure(e => e == 3);

            Assert.That(result, Is.AssignableTo<Result<string, bool>>());
            Assert.That(result.UnwrapError(), Is.EqualTo(true));
        }
    }
}
