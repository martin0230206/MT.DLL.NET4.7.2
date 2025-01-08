using System.Web;
//using MT.Extensions;
using MT.Utilities.UploadFile.Extensions;
using MT.Utilities.UploadFile.Interfaces;

namespace MT.Utilities.UploadFile.Vaildators
{
	/// <summary>
	/// 檢查檔案大小的驗證器
	/// </summary>
	public class FileSizeValidator : IValidator
	{
		private readonly int _maxSizeKB;

		/// <summary>
		/// 初始化檔案大小驗證器
		/// </summary>
		/// <param name="maxSize_KB">檔案大小上限(KB)</param>
		public FileSizeValidator(int maxSize_KB)
		{
			_maxSizeKB = maxSize_KB * 1024;
		}

		/// <summary>
		/// 驗證上傳檔案的大小是否超過限制
		/// </summary>
		/// <param name="file">要驗證的上傳檔案</param>
		/// <returns>
		/// - 如果沒有上傳檔案，返回成功結果
		/// - 如果檔案大小超過限制，返回失敗結果及錯誤訊息
		/// - 如果檔案大小在限制內，返回成功結果
		/// </returns>
		public ValidationResult Validate(IMyFile file)
		{
			if (file.HasFile() == false)
			{
				return ValidationResult.Ok();
			}

			if (file.ContentLength > _maxSizeKB)
			{
				return ValidationResult.Fail($"檔案超過限制 {_maxSizeKB} bytes");
			}

			return ValidationResult.Ok();
		}
	}
}
