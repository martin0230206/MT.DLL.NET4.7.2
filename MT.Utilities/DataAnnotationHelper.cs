using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Utilities
{
	public static class DataAnnotationHelper
	{
		public const string DateFormat = "{0:yyyy/MM/dd}";
		public const string RequiredError = "{0} 必填";
		public const string EmailAddressError = "{0} 電子郵件格式有誤";
		public const string StringMaxLengthError = "{0}不得超過 {1} 個字元";
		public const string StringLengthRangeError = "{0}長度為 {2}~{1} 個字元";
	}
}
