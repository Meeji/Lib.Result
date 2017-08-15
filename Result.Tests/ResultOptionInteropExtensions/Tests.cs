namespace System1Group.Core.Result.Tests.ResultOptionInteropExtensions
{
    using NUnit.Framework;
    using Optional;
    using Optional.Unsafe;
    using System1Group.Core.Result.ResultOptionInteropExtensions;

    [TestFixture]
    public class Result_ResultOptionInteropExtensions_Tests
    {
        [Test]
        public void UnwrapOption_Success()
        {
            var opt = Option.Some(5);
            var res = Result.Success<Option<int>, string>(opt).UnwrapOption();
            Assert.That(res, Is.EqualTo(opt));
        }

        [Test]
        public void UnwrapOption_Failure()
        {
            var res = Result.Failure<Option<int>, string>("some_string").UnwrapOption();
            Assert.That(res.HasValue, Is.False);
        }

        [Test]
        public void ToOption_Success()
        {
            var res = Result.Success<int, string>(5).ToOption();
            Assert.That(res.ValueOrFailure(), Is.EqualTo(5));
        }

        [Test]
        public void ToOption_Failure()
        {
            var res = Result.Failure<int, string>("some_string").ToOption();
            Assert.That(res.HasValue, Is.False);
        }

        [Test]
        public void ToResult_Value_Some()
        {
            var res = Option.Some(5).ToResult("some_string");
            Assert.That(res.Unwrap(), Is.EqualTo(5));
        }

        [Test]
        public void ToResult_Value_None()
        {
            var res = Option.None<int>().ToResult("some_string");
            Assert.That(res.UnwrapError(), Is.EqualTo("some_string"));
        }

        [Test]
        public void ToResult_Func_Some()
        {
            var res = Option.Some(5).ToResult(() => "some_string");
            Assert.That(res.Unwrap(), Is.EqualTo(5));
        }

        [Test]
        public void ToResult_Func_None()
        {
            var res = Option.None<int>().ToResult(() => "some_string");
            Assert.That(res.UnwrapError(), Is.EqualTo("some_string"));
        }

        [Test]
        public void Match_Success_Some()
        {
            var res = Result.Success<Option<int>, string>(Option.Some(5)).Match(i => i.ToString(), () => "some_string");
            Assert.That(res, Is.EqualTo("5"));
        }

        [Test]
        public void Match_Success_None()
        {
            var res = Result.Success<Option<int>, string>(Option.None<int>()).Match(i => i.ToString(), () => "some_string");
            Assert.That(res, Is.EqualTo("some_string"));
        }

        [Test]
        public void Match_Failure()
        {
            var res = Result.Failure<Option<int>, string>("some_other_string").Match(i => i.ToString(), () => "some_string");
            Assert.That(res, Is.EqualTo("some_string"));
        }

        [Test]
        public void Do_Some_Success()
        {
            var res = Option.Some(Result.Success<int, string>(5)).Do(i => i.ToString(), f => f + "!", "some_string");
            Assert.That(res, Is.EqualTo("5"));
        }

        [Test]
        public void Do_Some_Failure()
        {
            var res = Option.Some(Result.Failure<int, string>("another_string")).Do(i => i.ToString(), f => f + "!", "some_string");
            Assert.That(res, Is.EqualTo("another_string!"));
        }

        [Test]
        public void Do_None()
        {
            var res = Option.None<Result<int, string>>().Do(i => i.ToString(), f => f + "!", "some_string");
            Assert.That(res, Is.EqualTo("some_string!"));
        }
    }
}
