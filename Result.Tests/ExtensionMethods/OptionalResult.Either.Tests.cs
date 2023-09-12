namespace Result.Tests;

using NUnit.Framework;

[TestFixture]
public class OptionalResult_Either_Tests
{
    [Test]
    public void Ok_WithoutType_Success()
    {
        var testClass = new TestClassA();
        var result = Result.Success<TestClassA, TestClassB>(testClass);

        Assert.That(result.Either(), Is.EqualTo(testClass));
    }

    [Test]
    public void Ok_WithoutType_Failure()
    {
        var testClass = new TestClassB();
        var result = Result.Failure<TestClassA, TestClassB>(testClass);

        Assert.That(result.Either(), Is.EqualTo(testClass));
    }

    [Test]
    public void Ok_WithSameType_Success()
    {
        var testClass = new TestClassA();
        var result = Result.Success<TestClassA, TestClassA>(testClass);

        Assert.That(result.Either<TestClassA>(), Is.EqualTo(testClass));
    }

    [Test]
    public void Ok_WithSameType_Failure()
    {
        var testClass = new TestClassA();
        var result = Result.Failure<TestClassA, TestClassA>(testClass);

        Assert.That(result.Either<TestClassA>(), Is.EqualTo(testClass));
    }

    private class TestClassA
    {
    }

    private class TestClassB
    {
    }
}
