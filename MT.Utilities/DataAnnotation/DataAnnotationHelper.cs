using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Utilities.DataAnnotation
{
    /// <summary>
    /// 提供資料註解相關的輔助常數和錯誤訊息模板。
    /// </summary>
    public static class DataAnnotationHelper
    {
        /// <summary>
        /// 日期格式化模板，預設格式為 "yyyy/MM/dd"。
        /// </summary>
        public const string DateFormat = "{0:yyyy/MM/dd}";

        /// <summary>
        /// 必填欄位錯誤訊息模板，例如："欄位名稱 必填"。
        /// </summary>
        public const string RequiredError = "{0} 必填";

        /// <summary>
        /// 電子郵件格式錯誤訊息模板，例如："欄位名稱 電子郵件格式有誤"。
        /// </summary>
        public const string EmailAddressError = "{0} 電子郵件格式有誤";

        /// <summary>
        /// 字串最大長度錯誤訊息模板，例如："欄位名稱不得超過 X 個字元"。
        /// </summary>
        public const string StringMaxLengthError = "{0}不得超過 {1} 個字元";

        /// <summary>
        /// 字串長度範圍錯誤訊息模板，例如："欄位名稱長度為 X~Y 個字元"。
        /// </summary>
        public const string StringLengthRangeError = "{0}長度為 {2}~{1} 個字元";
    }

}
