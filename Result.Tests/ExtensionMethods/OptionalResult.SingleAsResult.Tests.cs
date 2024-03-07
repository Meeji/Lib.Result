using Result.Unsafe;

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

    [TestCase(null, CollectionError.IsNull, TestName = "Null Collection")]
    [TestCase(new int[0], CollectionError.IsEmpty, TestName = "Empty Collection")]
    [TestCase(new[] { 1, 2, 3 }, CollectionError.MultipleMatchingItems, TestName = "Multi-item Collection")]
    public void ErrorCases(IEnumerable<int> collection, CollectionError expectedResult)
    {
        Assert.That(collection.SingleAsResult().UnwrapError(), Is.EqualTo(expectedResult));
    }
    
    [Test]
    public void Ok_FirstItem_WithFunc()
    {
        var result = new[] { 1, 2, 3 }.SingleAsResult(i => i == 1);

        Assert.That(result.Unwrap(), Is.EqualTo(1));
    }
    
    [Test]
    public void Ok_WithFunc()
    {
        var result = new[] { 1, 2, 3 }.SingleAsResult(i => i == 2);

        Assert.That(result.Unwrap(), Is.EqualTo(2));
    }
    
    [Test]
    public void Ok_LastItem_WithFunc()
    {
        var result = new[] { 1, 2, 3 }.SingleAsResult(i => i == 3);

        Assert.That(result.Unwrap(), Is.EqualTo(3));
    }
    
    [TestCase(null, CollectionError.IsNull, TestName = "Null Collection")]
    [TestCase(new int[0], CollectionError.IsEmpty, TestName = "Empty Collection")]
    [TestCase(new[] { 1, 3, 4 }, CollectionError.NoMatchingItems, TestName = "No matching item Collection")]
    [TestCase(new[] { 1, 2, 2, 3 }, CollectionError.MultipleMatchingItems, TestName = "Multi-item Collection")]
    [TestCase(new[] { 2, 2, 3, 4 }, CollectionError.MultipleMatchingItems, TestName = "Multi-item Collection 2")]
    [TestCase(new[] { 2, 3, 4, 2 }, CollectionError.MultipleMatchingItems, TestName = "Multi-item Collection 3")]

    public void ErrorCases_WithFunc(IEnumerable<int> collection, CollectionError expectedResult)
    {
        Assert.That(collection.SingleAsResult(i => i == 2).UnwrapError(), Is.EqualTo(expectedResult));
    }
}
