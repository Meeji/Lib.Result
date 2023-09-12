namespace Result.Tests;

using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class OptionalResult_SingleAsResult_Tests
{
    [Test]
    public void Ok()
    {
        var result = new[] { 1 }.SingleAsResult();

        Assert.That(result.Unwrap(), Is.EqualTo(1));
    }

    [TestCase(null, "SingleAsResult called on null collection", TestName = "Null Collection")]
    [TestCase(new int[0], "SingleAsResult called on collection with no elements", TestName = "Empty Collection")]
    [TestCase(new[] { 1, 2, 3 }, "SingleAsResult called on collection with more than one element", TestName = "Multi-item Collection")]
    public void ErrorCases(IEnumerable<int> collection, string expectedResult)
    {
        Assert.That(collection.SingleAsResult().UnwrapError(), Is.EqualTo(expectedResult));
    }
}
