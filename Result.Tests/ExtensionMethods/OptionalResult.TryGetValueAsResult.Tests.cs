namespace Result.Tests;

using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class OptionalResult_TryGetValueAsResult_Tests
{
    [Test]
    public void Ok()
    {
        var dict = new Dictionary<int, int> { { 1, 10 }, { 2, 20 }, { 3, 30 } };
        Assert.That(dict.TryGetValueAsResult(1).Unwrap(), Is.EqualTo(10));
        Assert.That(dict.TryGetValueAsResult(2).Unwrap(), Is.EqualTo(20));
    }

    [Test]
    public void NullDictionary()
    {
        IDictionary<int, int>? dict = null;

        // ReSharper disable once ExpressionIsAlwaysNull
        Assert.That(dict!.TryGetValueAsResult(1).UnwrapError(), Is.EqualTo("Could not get value from null dictionary"));
    }

    [Test]
    public void NoMatchingKey()
    {
        var dict = new Dictionary<int, int>();
        Assert.That(dict.TryGetValueAsResult(1).UnwrapError(), Is.EqualTo("Dictionary does not contain key: 1"));
    }
}
