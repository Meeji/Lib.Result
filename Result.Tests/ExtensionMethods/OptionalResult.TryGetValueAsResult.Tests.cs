namespace Result.Tests
{
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_TryGetValueAsResult_Tests
    {
        [Test]
        public void Ok()
        {
            var dict = new Dictionary<int, int> { { 1, 10 }, { 2, 20 }, { 3, 30 } };
            Assert.AreEqual(10, dict.TryGetValueAsResult(1).Unwrap());
            Assert.AreEqual(20, dict.TryGetValueAsResult(2).Unwrap());
        }

        [Test]
        public void NullDictionary()
        {
            IDictionary<int, int> dict = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.AreEqual("Could not get value from null dictionary", dict.TryGetValueAsResult(1).UnwrapError());
        }

        [Test]
        public void NoMatchingKey()
        {
            var dict = new Dictionary<int, int>();
            Assert.AreEqual("Dictionary does not contain key: 1", dict.TryGetValueAsResult(1).UnwrapError());
        }
    }
}
