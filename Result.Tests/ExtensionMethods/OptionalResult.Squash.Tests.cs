using Result.Unsafe;

namespace Result.Tests;

using NUnit.Framework;

[TestFixture]
public class OptionalResult_Squash_Tests
{
    [Test]
    public void Ok_SquashSuccess()
    {
        var result = new Success<int, string>(9);
        var nestedResult = new Success<Result<int, string>, string>(result);

        var squashed = nestedResult.Flatten();
        Assert.That(result, Is.EqualTo(squashed));
    }

    [Test]
    public void Ok_SquashFailure()
    {
        var result = "bad things";
        var nestedResult = new Failure<Result<int, string>, string>(result);

        var squashed = nestedResult.Flatten().UnwrapError();
        Assert.That(result, Is.EqualTo(squashed));
    }
    
    [Test]
    public void Ok_Enumerable_Empty()
    {
        List<Result<int, string>> result = [ ];

        var squashed = result.Flatten();
        Assert.That(squashed.Unwrap(), Is.EqualTo(Array.Empty<int>()));
    }
    
    [Test]
    public void Ok_Enumerable_SingleSuccess()
    {
        Result<int, string>[] result = [ 9 ];

        var squashed = result.Flatten();
        Assert.That(squashed.Unwrap(), Is.EqualTo(new[] { 9 }));
    }
    
    [Test]
    public void Ok_Enumerable_Successes()
    {
        Result<int, string>[] result = [ 9, 8, 7, 6 ];

        var squashed = result.Flatten();
        Assert.That(squashed.Unwrap(), Is.EqualTo(new[] { 9, 8, 7, 6 }));
    }
    
    [Test]
    public void Ok_Enumerable_SingleFailure()
    {
        Result<int, string>[] result = [ "foo" ];

        var squashed = result.Flatten();
        Assert.That(squashed.UnwrapError(), Is.EqualTo("foo"));
    }
    
    [Test]
    public void Ok_Enumerable_MultipleFailures()
    {
        Result<int, string>[] result = [ "foo", "bar", "baz" ];

        var squashed = result.Flatten();
        Assert.That(squashed.UnwrapError(), Is.EqualTo("foo"));
    }
    
    [Test]
    public void Ok_Enumerable_Mix()
    {
        Result<int, string>[] result = [ 1, 2, 3, "foo", 4, 5, "bar", 6 ];

        var squashed = result.Flatten();
        Assert.That(squashed.UnwrapError(), Is.EqualTo("foo"));
    }
}
