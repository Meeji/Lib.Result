namespace System1Group.Core.Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_UnwrapAll_Tests
    {
        [Test]
        public void Ok_OneSuccess()
        {
            var initialCollection = new Result<int, string>[] { new Success<int, string>(5) };
            var expectedArray = new[] { 5 };

            var result = initialCollection.UnwrapAll();

            Assert.True(result.IsSuccess);
            CollectionAssert.AreEqual(expectedArray, result.Unwrap());
        }

        [Test]
        public void Ok_OneFailure()
        {
            var initialCollection = new Result<int, string>[] { new Failure<int, string>("Oops") };

            var result = initialCollection.UnwrapAll();

            Assert.True(result.IsFailure);
            CollectionAssert.AreEqual("Oops", result.UnwrapError());
        }

        [Test]
        public void Ok_AllSuccess()
        {
            var initialCollection = new Result<int, string>[]
                              {
                                  new Success<int, string>(5),
                                  new Success<int, string>(7),
                                  new Success<int, string>(9)
                              };
            var expectedArray = new[] { 5, 7, 9 };

            var result = initialCollection.UnwrapAll();

            Assert.True(result.IsSuccess);
            CollectionAssert.AreEqual(expectedArray, result.Unwrap());
        }

        [Test]
        public void Ok_MixedValues()
        {
            var initialCollection = new Result<int, string>[]
                              {
                                  new Success<int, string>(5),
                                  new Success<int, string>(7),
                                  new Failure<int, string>("Oops"),
                                  new Success<int, string>(9),
                                  new Failure<int, string>("Another error")
                              };

            var result = initialCollection.UnwrapAll();

            Assert.True(result.IsFailure);
            CollectionAssert.AreEqual("Oops", result.UnwrapError());
        }
    }
}
