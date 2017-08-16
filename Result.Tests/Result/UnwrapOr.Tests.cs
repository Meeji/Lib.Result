namespace System1Group.Lib.Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_UnwrapOr_Tests
    {
        [Test]
        public void Success_Ok()
        {
            var obj = new { field = "field" };
            var success = new Success<object, string>(obj);
            var obj2 = success.UnwrapOr(new object());

            Assert.That(obj, Is.EqualTo(obj2));
        }

        [Test]
        public void Failure_Ok()
        {
            var error = "error message";
            var obj = new { field = "field" };
            var failure = new Failure<object, string>(error);

            var obj2 = failure.UnwrapOr(obj);
            Assert.That(obj, Is.EqualTo(obj2));
        }

        [Test]
        public void Success_WithFunc_Ok()
        {
            var obj = new { field = "field" };
            var success = new Success<object, string>(obj);
            var obj2 = success.UnwrapOr(() => new object());

            Assert.That(obj, Is.EqualTo(obj2));
        }

        [Test]
        public void Failure_WithFunc_Ok()
        {
            var error = "error message";
            var obj = new { field = "field" };
            var failure = new Failure<object, string>(error);

            var obj2 = failure.UnwrapOr(() => obj);
            Assert.That(obj, Is.EqualTo(obj2));
        }

        [Test]
        public void Success_WithArgumentFunc_Ok()
        {
            var obj = new { field = "field" };
            var success = new Success<object, string>(obj);
            var obj2 = success.UnwrapOr(failure => new { field = failure });

            Assert.That(obj, Is.EqualTo(obj2));
        }

        [Test]
        public void Failure_WithArgumentFunc_Ok()
        {
            var error = "error message";
            var failure = new Failure<object, string>(error);
            var obj = failure.UnwrapOr(err => err);
            Assert.That(error, Is.EqualTo(obj));
        }
    }
}
