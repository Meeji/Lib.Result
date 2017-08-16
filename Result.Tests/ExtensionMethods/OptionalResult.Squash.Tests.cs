namespace System1Group.Lib.Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_Squash_Tests
    {
        [Test]
        public void Ok_SquashSuccess()
        {
            var result = new Success<int, string>(9);
            var nestedResult = new Success<Result<int, string>, string>(result);

            var squashed = nestedResult.Squash();
            Assert.AreEqual(squashed, result);
        }

        [Test]
        public void Ok_SquashFailure()
        {
            var result = "bad things";
            var nestedResult = new Failure<Result<int, string>, string>(result);

            var squashed = nestedResult.Squash().UnwrapError();
            Assert.AreEqual(squashed, result);
        }
    }
}
