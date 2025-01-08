using System.IO;
using System.Linq;
using System.Web;
//using MT.Extensions;
using MT.Utilities.UploadFile.Extensions;
using MT.Utilities.UploadFile.Interfaces;

namespace MT.Utilities.UploadFile.Vaildators
{
    /// <summary>
    /// 驗證上傳檔案是否為允許的圖片格式
    /// </summary>
    public class ImageFileValidator : IValidator
	{
		/// <summary>
		/// 允許的檔案副檔名列表
		/// </summary>
		private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png" };

		/// <summary>
		/// 允許的 MIME 類型列表
		/// </summary>
		private static readonly string[] AllowedMimeTypes = { "image/jpeg", "image/png" };

		/// <summary>
		/// 驗證上傳檔案的副檔名與MIME類型是否為允許的圖片格式
		/// </summary>
		/// <param name="file">要驗證的上傳檔案</param>
		/// <returns>
		/// - 如果沒有上傳檔案，返回成功結果
		/// - 如果檔案格式符合允許的副檔名和MIME類型，返回成功結果
		/// - 如果檔案格式不符合允許的格式，返回失敗結果及錯誤訊息
		/// </returns>
		public ValidationResult Validate(IMyFile file)
		{
			if (file.HasFile() == false)
			{
				return ValidationResult.Ok();
			}

			// 檢查副檔名
			string ext = Path.GetExtension(file.FileName).ToLower();
			if (!AllowedExtensions.Contains(ext))
			{
				string allowedExts = string.Join(", ", AllowedExtensions);
				return ValidationResult.Fail($"檔案格式必須是{allowedExts}");
			}

			// 檢查 MIME 類型
			if (!AllowedMimeTypes.Contains(file.ContentType.ToLower()))
			{
				string allowedTypes = string.Join(", ", AllowedMimeTypes);
				return ValidationResult.Fail($"檔案類型必須是{allowedTypes}");
			}

			return ValidationResult.Ok();
		}

	}
}
