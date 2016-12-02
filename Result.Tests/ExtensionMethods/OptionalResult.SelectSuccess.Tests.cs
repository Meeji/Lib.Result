namespace Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_SelectSuccess_Tests
    {
        [Test]
        public void Ok()
        {
            var results = new Result<int, string>[]
                              {
                                  new Failure<int, string>("foo"), 
                                  new Success<int, string>(5), 
                                  new Failure<int, string>("foo"), 
                                  new Success<int, string>(7), 
                                  new Failure<int, string>("foo"), 
                                  new Success<int, string>(9)
                              };
            var expectedArray = new[] { 5, 7, 9 };

            CollectionAssert.AreEqual(expectedArray, results.SelectSuccess());
        }
    }
}
