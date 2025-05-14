using NUnit.Framework;
using MT.Extensions;
using System;
using System.Collections.Generic;

namespace MT.Extensions.Test.Enumerable
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [Test]
        public void IsAlmostIncreasing_完全遞增_回傳True()
        {
            var data = new List<double> { 1, 2, 3, 4, 5 };
            Assert.IsTrue(data.IsAlmostIncreasing());
        }

        [Test]
        public void IsAlmostIncreasing_允許一次小幅下降_回傳True()
        {
            var data = new List<double> { 1, 2, 1.95, 2.1 };
            Assert.IsTrue(data.IsAlmostIncreasing(0.1, 1));
        }

        [Test]
        public void IsAlmostIncreasing_超過允許下降次數_回傳False()
        {
            var data = new List<double> { 1, 0.8, 0.7 };
            Assert.IsFalse(data.IsAlmostIncreasing(0.1, 1));
        }

        [Test]
        public void IsAlmostIncreasing_空集合_回傳True()
        {
            var data = new List<double>();
            Assert.IsTrue(data.IsAlmostIncreasing());
        }

        [Test]
        public void IsAlmostIncreasing_傳入Null_拋出ArgumentNullException()
        {
            List<double> data = null;
            Assert.Throws<ArgumentNullException>(() => data.IsAlmostIncreasing());
        }
    }
}
