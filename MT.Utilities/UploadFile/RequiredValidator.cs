using System.Web;
using MT.Extensions;

namespace MT.Utilities.UploadFile
{
	/// <summary>
	/// 檢查必填的驗證器
	/// </summary>
	public class RequiredValidator : IValidator
	{
		/// <summary>
		/// 驗證檔案是否有上傳
		/// </summary>
		/// <param name="file">要驗證的上傳檔案</param>
		/// <returns>
		/// 如果有上傳檔案則返回成功結果，
		/// 如果沒有上傳檔案則返回失敗結果及錯誤訊息
		/// </returns>
		public ValidationResult Validate(HttpPostedFileBase file)
		{
			if (file.HasFile() == false)
			{
				return ValidationResult.Fail("請上傳檔案");
			}
			return ValidationResult.Ok();
		}
	}
}
