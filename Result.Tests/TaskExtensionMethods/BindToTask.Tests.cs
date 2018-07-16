namespace System1Group.Lib.Result.Tests.TaskExtensionMethods
{
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    public class TaskExtensionMethods_Tests
    {
        [Test]
        public async Task BindToTask()
        {
            var success = Result.Success<int, string>(12);
            var bound = success.BindToTask(i => Task.FromResult(i + 1));
            Assert.That((await bound).Unwrap(), Is.EqualTo(13));
        }

        [Test]
        public async Task BindToTask_FromTask()
        {
            var success = Task.FromResult(Result.Success<int, string>(12));
            var bound = success.BindToTask(i => Task.FromResult(i + 1));
            Assert.That((await bound).Unwrap(), Is.EqualTo(13));
        }

        [Test]
        public async Task BindToResultTask()
        {
            var success = Result.Success<int, string>(12);
            var bound = success.BindToResultTask(i => Task.FromResult(Result.Success<int, string>(i + 1)));
            Assert.That((await bound).Unwrap(), Is.EqualTo(13));
        }

        [Test]
        public async Task BindToResultTask_FromTask()
        {
            var success = Task.FromResult(Result.Success<int, string>(12));
            var bound = success.BindToResultTask(i => Task.FromResult(Result.Success<int, string>(i + 1)));
            Assert.That((await bound).Unwrap(), Is.EqualTo(13));
        }
    }
}
