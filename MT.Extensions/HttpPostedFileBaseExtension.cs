using System.Web;

namespace MT.Extensions
{
	/// <summary>
	/// 提供 HttpPostedFileBase 的擴充方法
	/// </summary>
	public static class HttpPostedFileBaseExtension
	{
		/// <summary>
		/// 檢查上傳的檔案是否有效
		/// </summary>
		/// <param name="file">要檢查的 HttpPostedFileBase 物件</param>
		/// <returns>
		/// 如果檔案有效則返回 true，否則返回 false
		/// 以下情況會返回 false：
		/// - 檔案為 null
		/// - 檔案內容長度為 0
		/// - 檔案名稱為空或 null
		/// </returns>
		public static bool HasFile(this HttpPostedFileBase file)
		{
			return !(
				file == null ||
				file.ContentLength == 0 ||
				string.IsNullOrEmpty(file.FileName)
				);
		}
	}
}
