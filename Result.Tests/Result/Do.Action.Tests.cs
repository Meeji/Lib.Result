namespace Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_Do_Action_Tests
    {
        [Test]
        public void Success_Ok()
        {
            var obj = new { field = "field" };
            var success = new Success<object, string>(obj);

            var successCalled = false;
            var failureCalled = false;

            success.Do(
                onSuccess: o => { successCalled = true; },
                onFailure: err => { failureCalled = true; });

            Assert.That(successCalled, Is.True);
            Assert.That(failureCalled, Is.False);
        }

        [Test]
        public void Failure_Ok()
        {
            var error = "error message";
            var success = new Failure<object, string>(error);

            var successCalled = false;
            var failureCalled = false;

            success.Do(
                onSuccess: o => { successCalled = true; },
                onFailure: err => { failureCalled = true; });

            Assert.That(successCalled, Is.False);
            Assert.That(failureCalled, Is.True);
        }
    }
}
