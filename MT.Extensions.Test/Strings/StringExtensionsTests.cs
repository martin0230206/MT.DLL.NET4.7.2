using NUnit.Framework;
using MT.Extensions;
using System;

namespace MT.Extensions.Test.Strings
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void TryParseInt_有效整數字串_回傳正確整數()
        {
            Assert.AreEqual(123, "123".TryParseInt());
        }

        [Test]
        public void TryParseInt_無效整數字串_回傳Null()
        {
            Assert.IsNull("abc".TryParseInt());
        }

        [Test]
        public void TryParseDateTime_有效日期字串_回傳正確日期()
        {
            var result = "2024-05-14".TryParseDateTime();
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(new DateTime(2024, 5, 14), result.Value.Date);
        }

        [Test]
        public void TryParseDateTime_無效日期字串_回傳Null()
        {
            Assert.IsNull("notadate".TryParseDateTime());
        }

        [Test]
        public void Truncate_字串長度大於指定長度_回傳截斷字串()
        {
            Assert.AreEqual("Hello", "HelloWorld".Truncate(5));
        }

        [Test]
        public void Truncate_字串長度小於指定長度_回傳原字串()
        {
            Assert.AreEqual("Hi", "Hi".Truncate(5));
        }

        [Test]
        public void Truncate_字串長度大於指定長度加Ellipsis_回傳截斷加省略號()
        {
            Assert.AreEqual("Hello...", "HelloWorld".Truncate(5, true));
        }

        [Test]
        public void Truncate_空字串_回傳空字串()
        {
            Assert.AreEqual(string.Empty, "".Truncate(5));
        }
    }
}
