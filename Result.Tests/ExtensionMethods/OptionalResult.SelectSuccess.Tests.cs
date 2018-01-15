namespace System1Group.Lib.Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_Select_Tests
    {
        [Test]
        public void SelectSuccess_Ok()
        {
            var results = new Result<int, string>[] { "foo", 5, "foo", 7, "foo", 9 };
            var expectedArray = new[] { 5, 7, 9 };

            CollectionAssert.AreEqual(expectedArray, results.SelectSuccess());
        }

        [Test]
        public void SelectFailure_Ok()
        {
            var results = new Result<string, int>[] { "foo", 5, "foo", 7, "foo", 9 };
            var expectedArray = new[] { 5, 7, 9 };

            CollectionAssert.AreEqual(expectedArray, results.SelectFailure());
        }
    }
}
