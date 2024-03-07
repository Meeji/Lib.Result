using NUnit.Framework;
using Result.Unsafe;

namespace Result.Tests;

[TestFixture]
public class AggregateAsync_Tests
{
    [Test]
    public async Task Ok_NoItems()
    {
        var input = Result.Success<IEnumerable<int>, string>(Array.Empty<int>());
        var seed = 1M;

        var output = await input.AggregateAsync(seed, i => Task.FromResult((Result<decimal, string>)(decimal)i));
        
        Assert.That(output.Unwrap(), Is.EqualTo(seed));
    }
    
    [Test]
    public async Task Ok_OneItems()
    {
        var input = Result.Success<IEnumerable<int>, string>(new[] { 2 });
        var seed = 1M;

        var output = await input.AggregateAsync(seed, i => Task.FromResult((Result<decimal, string>)(decimal)i));
        
        Assert.That(output.Unwrap(), Is.EqualTo(2M));
    }
    
    [Test]
    public async Task Ok_MultipleItems()
    {
        var input = Result.Success<IEnumerable<int>, string>(new[] { 2, 3, 4 });
        var seed = 1M;

        var output = await input.AggregateAsync(seed, i => Task.FromResult((Result<decimal, string>)(decimal)i));
        
        Assert.That(output.Unwrap(), Is.EqualTo(4M));
    }
    
    [Test]
    public async Task Ok_MultipleItemsWithAggregator()
    {
        var input = Result.Success<IEnumerable<int>, string>(new[] { 2, 3, 4 });
        var seed = 1M;

        var output = await input.AggregateAsync(
            seed, 
            i => Task.FromResult((Result<decimal, string>)(decimal)i), 
            (i, j) => i + j);
        
        Assert.That(output.Unwrap(), Is.EqualTo(10M));
    }

    
    [Test]
    public async Task Ok_FailureType()
    {
        var input = Result.Failure<IEnumerable<int>, string>("foo");
        var seed = 1M;

        var output = await input.AggregateAsync(seed, i => Task.FromResult((Result<decimal, string>)(decimal)i));
        
        Assert.That(output.UnwrapError(), Is.EqualTo("foo"));
    }
    
    [Test]
    public async Task Ok_OneFailure()
    {
        var input = Result.Success<IEnumerable<int>, string>(new[] { 2, 3, 4 });
        var seed = 1M;

        var output = await input.AggregateAsync(seed, i => Task.FromResult(i == 3 ? (Result<decimal, string>)"foo" : (decimal)i));
        
        Assert.That(output.UnwrapError(), Is.EqualTo("foo"));
    }
}