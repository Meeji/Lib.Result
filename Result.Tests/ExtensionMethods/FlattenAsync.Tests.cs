using Result.Unsafe;

namespace Result.Tests;

using NUnit.Framework;

[TestFixture]
public class FlattenAsync_Tests
{
    [Test]
    public async Task Ok_Enumerable_Empty()
    {
        List<Task<Result<int, string>>> result = [ ];

        var squashed = await result.FlattenAsync();
        Assert.That(squashed.Unwrap(), Is.EqualTo(Array.Empty<int>()));
    }
    
    [Test]
    public async Task Ok_Enumerable_SingleSuccess()
    {
        var result = ToTasks([ (Result<int, string>)9 ]);

        var squashed = await result.FlattenAsync();
        Assert.That(squashed.Unwrap(), Is.EqualTo(new[] { 9 }));
    }
    
    [Test]
    public async Task Ok_Enumerable_Successes()
    {
        var result = ToTasks([ (Result<int, string>)9, 8, 7, 6 ]);

        var squashed = await result.FlattenAsync();
        Assert.That(squashed.Unwrap(), Is.EqualTo(new[] { 9, 8, 7, 6 }));
    }
    
    [Test]
    public async Task Ok_Enumerable_SingleFailure()
    {
        var result = ToTasks([ (Result<int, string>)"foo" ]);

        var squashed = await result.FlattenAsync();
        Assert.That(squashed.UnwrapError(), Is.EqualTo("foo"));
    }
    
    [Test]
    public async Task Ok_Enumerable_MultipleFailures()
    {
        var result = ToTasks([ (Result<int, string>)"foo", "bar", "baz" ]);

        var squashed = await result.FlattenAsync();
        Assert.That(squashed.UnwrapError(), Is.EqualTo("foo"));
    }
    
    [Test]
    public async Task Ok_Enumerable_Mix()
    {
        var result = ToTasks([ (Result<int, string>)1, 2, 3, "foo", 4, 5, "bar", 6 ]);

        var squashed = await result.FlattenAsync();
        Assert.That(squashed.UnwrapError(), Is.EqualTo("foo"));
    }

    private static IEnumerable<Task<T>> ToTasks<T>(IEnumerable<T> enumerable) => enumerable.Select(Task.FromResult);
}
