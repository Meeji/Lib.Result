namespace Result.Tests
{
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_SingleAsResult_Tests
    {
        [Test]
        public void Ok()
        {
            var result = new[] { 1 }.SingleAsResult();

            Assert.AreEqual(result.Unwrap(), 1);
        }

        [TestCase(null, ExpectedResult = "SingleAsResult called on null collection", TestName = "Null Collection")]
        [TestCase(new int[0], ExpectedResult = "SingleAsResult called on collection with no elements", TestName = "Empty Collection")]
        [TestCase(new[] { 1, 2, 3 }, ExpectedResult = "SingleAsResult called on collection with more than one element", TestName = "Multi-item Collection")]
        public string ErrorCases(IEnumerable<int> collection)
        {
            return collection.SingleAsResult().UnwrapError();
        }
    }
}
