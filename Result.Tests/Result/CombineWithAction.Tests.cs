namespace Result.Tests
{
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_CombineWithAction_Tests
    {
        [Test]
        public void Success_Ok()
        {
            var baseList = new Success<List<int>, string>(new List<int> { 1, 2, 3 });
            var addInt = new Success<int, string>(4);

#pragma warning disable 618
            baseList.Combine(addInt, (list, i) => list.Add(i));
#pragma warning restore 618

            CollectionAssert.AreEquivalent(baseList.Unwrap(), new[] { 1, 2, 3, 4 });
        }

        [Test]
        public void Failure_Ok()
        {
            var success = new Success<int, string>(5);
            var failure = new Failure<int, string>("Failure");

#pragma warning disable CS0618 // Type or member is obsolete
            var result1 = success.Combine(failure, (s, f) => { s = s + f; });
            var result2 = failure.Combine(success, (f, s) => { s = s + f; });
#pragma warning restore CS0618 // Type or member is obsolete

            Assert.That(result1.UnwrapError(), Is.EqualTo("Failure"));
            Assert.That(result2.UnwrapError(), Is.EqualTo("Failure"));
        }
    }
}
