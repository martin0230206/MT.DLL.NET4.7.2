using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Extensions
{
	public static class EnumerableExtensions
	{
		/// <summary>
		/// 判斷一個 double 序列是否為「幾乎遞增」的序列。
		/// 容許每次下降不超過指定百分比，且整體下降次數不得超過指定上限。
		/// </summary>
		/// <param name="source">double 序列</param>
		/// <param name="tolerance">
		/// 容忍下降比例（例如 0.1 表示可接受最多 10% 的下降），預設為 0（完全不接受下降）
		/// </param>
		/// <param name="maxDropCount">
		/// 整體允許下降的次數上限，預設為 0（完全不允許下降次數）
		/// </param>
		/// <returns>若符合條件則回傳 true，否則回傳 false</returns>
		public static bool IsAlmostIncreasing(
			this IEnumerable<double> source,
			double tolerance = 0,
			int maxDropCount = 0)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			var enumerator = source.GetEnumerator();

			if (!enumerator.MoveNext())
				return true;

			double previous = enumerator.Current;
			int dropCount = 0;

			while (enumerator.MoveNext())
			{
				double current = enumerator.Current;

				if (IsDropBeyondTolerance(previous, current, tolerance))
				{
					dropCount++;
					if (dropCount > maxDropCount)
						return false;
				}

				previous = current;
			}

			return true;
		}

		/// <summary>
		/// 判斷 current 是否比 previous 下降超出容忍範圍。
		/// </summary>
		private static bool IsDropBeyondTolerance(double previous, double current, double tolerance)
		{
			double minimumAcceptable = previous * (1 - tolerance);
			return current < minimumAcceptable;
		}
	}
}
