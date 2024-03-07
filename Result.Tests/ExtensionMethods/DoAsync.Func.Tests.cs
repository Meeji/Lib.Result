namespace Result.Tests;

using NUnit.Framework;

[TestFixture]
public class OptionalResult_DoAsync_Func_Tests
{
    private const string Error = "error";

    private static readonly object Obj = new object();

    private readonly Task<Result<object, string>> failure = Task.FromResult(Result.Failure<object, string>(Error));

    private readonly Task<Result<object, string>> success = Task.FromResult(Result.Success<object, string>(Obj));

    [Test]
    public async Task Success_Ok()
    {
        var result = await this.success.DoAsync(
            onSuccess: o => o,
            onFailure: err => new object());

        Assert.That(result, Is.EqualTo(Obj));
    }

    [Test]
    public async Task  Failure_Ok()
    {
        var result = await this.failure.DoAsync(
            onSuccess: o => string.Empty,
            onFailure: err => err);

        Assert.That(result, Is.EqualTo(Error));
    }
    
    [Test]
    public async Task Success_Task_Ok()
    {
        var result = await this.success.DoAsync(
            onSuccess: o => Task.FromResult(o),
            onFailure: err => Task.FromResult(new object()));

        Assert.That(result, Is.EqualTo(Obj));
    }

    [Test]
    public async Task  Failure_Task_Ok()
    {
        var result = await this.failure.DoAsync(
            onSuccess: o => Task.FromResult(string.Empty),
            onFailure: err => Task.FromResult(err));

        Assert.That(result, Is.EqualTo(Error));
    }
}