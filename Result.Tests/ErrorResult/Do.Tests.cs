namespace System1Group.Lib.Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ErrorResult_Do_Tests
    {
        [Test]
        public void Ok_Success()
        {
            var success = ErrorResult.NoError<string>();
            var result = success.Do(t => t, f => false);
            Assert.That(result, Is.True);
        }

        [Test]
        public void Ok_Failure()
        {
            var success = ErrorResult.WithError("error");
            var result = success.Do(t => t.ToString(), f => f.ToUpper());
            Assert.That(result, Is.EqualTo("ERROR"));
        }
    }
}
