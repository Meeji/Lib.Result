using Result.Unsafe;

namespace Result.Tests.TaskExtensionMethods;

using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class TaskExtensionMethods_Tests
{
    [Test]
    public async Task MapAsync()
    {
        var success = Result.Success<int, string>(12);
        var bound = success.ThenAsync(i => Task.FromResult(i + 1));
        Assert.That((await bound).Unwrap(), Is.EqualTo(13));
    }

    [Test]
    public async Task MapAsync_FromTask()
    {
        var success = Task.FromResult(Result.Success<int, string>(12));
        var bound = success.ThenAsync(i => Task.FromResult(i + 1));
        Assert.That((await bound).Unwrap(), Is.EqualTo(13));
    }

    [Test]
    public async Task MapToResultAsync()
    {
        var success = Result.Success<int, string>(12);
        var bound = success.ThenAsync(i => Task.FromResult(Result.Success<int, string>(i + 1)));
        Assert.That((await bound).Unwrap(), Is.EqualTo(13));
    }

    [Test]
    public async Task MapToResultAsync_FromTask()
    {
        var success = Task.FromResult(Result.Success<int, string>(12));
        var bound = success.ThenAsync(i => Task.FromResult(Result.Success<int, string>(i + 1)));
        Assert.That((await bound).Unwrap(), Is.EqualTo(13));
    }
}
