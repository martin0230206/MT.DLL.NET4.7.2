namespace MT.Utilities.UploadFile.Interfaces
{
	/// <summary>
	/// 檔案驗證器的介面定義
	/// </summary>
	public interface IValidator
	{
		/// <summary>
		/// 驗證上傳的檔案
		/// </summary>
		/// <param name="file">要驗證的上傳檔案</param>
		/// <returns>驗證結果，成功或失敗訊息</returns>
		ValidationResult Validate(IMyFile file);
	}
}
