namespace System1Group.Core.Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ResultGuard_FluentInterface_Tests
    {
        [Test]
        public void Ok()
        {
            // Slightly odd test - here to make sure the fluent interface isn't broken by any changes. No way to automatically test this 'properly'
            Result<int, string> wrappedResult = new Success<int, string>(10);
            IGuardEntryPoint<int, string, string> guard = new ResultGuard<int, string, string>(wrappedResult);

            var result = guard
                //// Set up some success paths ( we could also have set up failure paths first)
                .Success()
                .Where(i => i == 5, i => i.ToString() + " is 5!")
                .Where(i => i % 2 == 0, i => i.ToString() + " is even!")
                .Where(i => i < 0, i => i.ToString() + " is a negative number!")
                .Default(i => i + " is just some number") //// To start defining failure paths we NEED to provide a default value
                .Failure() //// Since we "closed" successes when we defined a default we can't go back later to add more paths
                .Where(i => i.Length > 100, i => i + " is a long message")
                .Where(i => i.StartsWith("Error"), i => "An error occurred: " + i)
                .Default(i => i + " is just some message") //// For safety we have to provide both a success and failure default so no paths are left uncovered
                //// .Success()                            //// Should not work - we've already defined our success paths!
                .Do();                                     //// Both defaults have been defined, only Do() is left

            Assert.AreEqual(result, "10 is even!");
        }

        [Test]
        public void MethodsThatReturnThis()
        {
            Result<int, string> wrappedResult = new Success<int, string>(10);
            var guard = new ResultGuard<int, string, string>(wrappedResult);

            Assert.AreSame(guard, ((IGuardEntryPoint<int, string, string>)guard).Success());
            Assert.AreSame(guard, ((IGuardFailureClosing<int, string, string>)guard).Success());
            Assert.AreSame(guard, ((IGuardEntryPoint<int, string, string>)guard).Failure());
            Assert.AreSame(guard, ((IGuardSuccessClosing<int, string, string>)guard).Failure());
        }
    }
}
