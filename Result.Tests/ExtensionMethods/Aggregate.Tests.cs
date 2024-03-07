using NUnit.Framework;
using Result.Unsafe;

namespace Result.Tests;

[TestFixture]
public class Aggregate_Tests
{
    [Test]
    public void Ok_NoItems()
    {
        var input = Result.Success<IEnumerable<int>, string>(Array.Empty<int>());
        var seed = 1M;

        var output = input.Aggregate(seed, i => (decimal)i);
        
        Assert.That(output.Unwrap(), Is.EqualTo(seed));
    }
    
    [Test]
    public void Ok_OneItems()
    {
        var input = Result.Success<IEnumerable<int>, string>(new[] { 2 });
        var seed = 1M;

        var output = input.Aggregate(seed, i => (decimal)i);
        
        Assert.That(output.Unwrap(), Is.EqualTo(2M));
    }
    
    [Test]
    public void Ok_MultipleItems()
    {
        var input = Result.Success<IEnumerable<int>, string>(new[] { 2, 3, 4 });
        var seed = 1M;

        var output = input.Aggregate(seed, i => (decimal)i);
        
        Assert.That(output.Unwrap(), Is.EqualTo(4M));
    }
    
    [Test]
    public void Ok_MultipleItemsWithAggregator()
    {
        var input = Result.Success<IEnumerable<int>, string>(new[] { 2, 3, 4 });
        var seed = 1M;

        var output = input.Aggregate(seed, i => (decimal)i, (i, j) => i + j);
        
        Assert.That(output.Unwrap(), Is.EqualTo(10M));
    }
    
    [Test]
    public void Ok_FailureType()
    {
        var input = Result.Failure<IEnumerable<int>, string>("foo");
        var seed = 1M;

        var output = input.Aggregate(seed, i => (decimal)i);
        
        Assert.That(output.UnwrapError(), Is.EqualTo("foo"));
    }
    
    [Test]
    public void Ok_OneFailure()
    {
        var input = Result.Success<IEnumerable<int>, string>(new[] { 2, 3, 4 });
        var seed = 1M;

        var output = input.Aggregate(seed, i => i == 3 ? "foo" : (decimal)i);
        
        Assert.That(output.UnwrapError(), Is.EqualTo("foo"));
    }
}