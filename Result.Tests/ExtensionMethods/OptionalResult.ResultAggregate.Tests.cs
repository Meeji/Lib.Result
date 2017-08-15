namespace System1Group.Core.Result.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class OptionalResult_ResultAggregate_Tests
    {
        [Test]
        public void Ok_WithDefault()
        {
            var results = new Result<int, string>[]
                              {
                                  new Success<int, string>(5),
                                  new Success<int, string>(7),
                                  new Success<int, string>(9)
                              };
            var expectedArray = new[] { 5, 7, 9 };

            var func = new Func<IList<int>, int, IList<int>>(
                (ints, i) =>
                    {
                        ints.Add(i);
                        return ints;
                    });

            CollectionAssert.AreEqual(expectedArray, results.ResultAggregate(new List<int>(), func).Unwrap());
        }

        [Test]
        public void Ok_WithoutDefault()
        {
            var results = new Result<int, string>[]
                              {
                                  new Success<int, string>(5),
                                  new Success<int, string>(7),
                                  new Success<int, string>(9)
                              };

            var func = new Func<int, int, int>((i1, i2) => i1 + i2);

            Assert.AreEqual(21, results.ResultAggregate(func).Unwrap());
        }

        [Test]
        public void WithFailure()
        {
            var results = new Result<int, string>[]
                              {
                                  new Success<int, string>(5),
                                  new Success<int, string>(7),
                                  new Success<int, string>(9),
                                  new Failure<int, string>("Error!"),
                              };

            var func = new Func<int, int, int>((i1, i2) => i1 + i2);

            Assert.AreEqual("Error!", results.ResultAggregate(func).UnwrapError());
        }

        [Test]
        public void NullValues()
        {
            var results = (IEnumerable<Result<int, string>>)null;
            var func = new Func<int, int, int>((i1, i2) => i1 + i2);

            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => results.ResultAggregate(func));
        }

        [Test]
        public void NullFunc()
        {
            var results = new Result<int, string>[] { new Success<int, string>(5), };
            var func = (Func<int, int, int>)null;

            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => results.ResultAggregate(func));
        }
    }
}
