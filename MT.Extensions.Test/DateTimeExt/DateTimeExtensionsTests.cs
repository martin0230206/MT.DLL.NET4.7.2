using NUnit.Framework;
using MT.Extensions;
using System;

namespace MT.Extensions.Test.DateTimeExt
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void GetPreviousWeekRange_週三_回傳正確範圍()
        {
            var date = new DateTime(2024, 5, 15); // Wed
            var (start, end) = date.GetPreviousWeekRange();
            Assert.AreEqual(new DateTime(2024, 5, 6), start); // 上週一
            Assert.AreEqual(new DateTime(2024, 5, 12, 23, 59, 59, 999).AddTicks(9999), end);
        }

        [Test]
        public void GetPreviousWeekRange_週日_回傳正確範圍()
        {
            var date = new DateTime(2024, 5, 19); // Sun
            var (start, end) = date.GetPreviousWeekRange();
            Assert.AreEqual(new DateTime(2024, 5, 6), start); // 上週一
            Assert.AreEqual(new DateTime(2024, 5, 12, 23, 59, 59, 999).AddTicks(9999), end);
        }
    }
}
