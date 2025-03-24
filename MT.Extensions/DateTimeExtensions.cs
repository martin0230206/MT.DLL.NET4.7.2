using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Extensions
{
    /// <summary>
    /// 擴充方法：用於 DateTime 類別，計算指定日期的前一週範圍。
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 取得指定日期的前一週範圍（從星期一開始到星期日結束）。
        /// </summary>
        /// <param name="date">指定的日期</param>
        /// <returns>回傳前一週的起始與結束日期範圍</returns>
        public static (DateTime Start, DateTime End) GetPreviousWeekRange(this DateTime date)
        {
            int daysSinceMonday = ((int)date.DayOfWeek + 6) % 7; // 計算與本週一的距離
            var start = date.AddDays(-daysSinceMonday - 7).Date; // 上週一
            var end = start.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999); // 上週日 23:59:59.999

            return (Start: start, End: end);
        }
    }
}
