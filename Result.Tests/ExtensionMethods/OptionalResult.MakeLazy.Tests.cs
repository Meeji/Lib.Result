namespace System1Group.Lib.Result.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_MakeLazy_Tests
    {
        [Test]
        public void Ok_MakeLazy_IsLazy()
        {
            var lazy = new LazyResult<int, string>(() => new Success<int, string>(5));
            var lazy2 = lazy.MakeLazy();
            Assert.AreEqual(lazy, lazy2);
        }

        [Test]
        public void Ok_MakeLazy_IsntLazy()
        {
            var result = new Success<int, string>(17);
            var lazyResult = result.MakeLazy();
            Assert.AreEqual(result.Unwrap(), lazyResult.Unwrap());
        }
    }
}
