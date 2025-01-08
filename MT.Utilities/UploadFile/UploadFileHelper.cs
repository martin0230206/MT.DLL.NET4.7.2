using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MT.Utilities.UploadFile.Extensions;
using MT.Utilities.UploadFile.Interfaces;

namespace MT.Utilities.UploadFile
{
	/// <summary>
	/// 處理檔案上傳的輔助類別，提供檔案驗證和儲存功能
	/// </summary>
	public class UploadFileHelper
	{
		/// <summary>
		/// 取得或設定驗證失敗時的錯誤訊息
		/// </summary>
		public string ErrorMessage { get; set; }

		/// <summary>
		/// 存放驗證器的集合
		/// </summary>
		private List<IValidator> _validators = new List<IValidator>();

		/// <summary>
		/// 新增一個驗證器到驗證器集合中
		/// </summary>
		/// <param name="validator">要添加的驗證器</param>
		/// <remarks>如果傳入的驗證器為 null，則不會被加入集合</remarks>
		public void AddValidator(IValidator validator)
		{
			if (validator == null)
			{
				return;
			}
			_validators.Add(validator);
		}

		/// <summary>
		/// 使用所有已加入的驗證器驗證上傳的檔案
		/// </summary>
		/// <param name="file">要驗證的上傳檔案</param>
		/// <returns>
		/// 如果所有驗證都通過則返回 true，
		/// 如果任一驗證失敗則返回 false 且設定 ErrorMessage
		/// </returns>
		public bool Validate(IMyFile file)
		{
			foreach (var validator in _validators)
			{
				ValidationResult result = validator.Validate(file);
				if (result.Success == false)
				{
					ErrorMessage = result.Message;
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 將上傳的檔案儲存到指定路徑，並以新的檔名命名
		/// </summary>
		/// <param name="path">檔案要儲存的路徑</param>
		/// <param name="file">要儲存的上傳檔案</param>
		/// <returns>
		/// 儲存成功則返回新的檔案名稱（GUID + 原始副檔名），
		/// 如果沒有檔案則返回空字串
		/// </returns>
		/// <remarks>
		/// 新檔名格式為：GUID（無連字符）+ 原始副檔名
		/// 例如：8d601c10e75a40959a613be6ec29c948.jpg
		/// </remarks>
		public string SaveAs(string path, IMyFile file)
		{
			if (file.HasFile() == false) return string.Empty;

			string fileName = file.FileName;
			string ext = Path.GetExtension(fileName).ToLower();
			string newFileName = $"{Guid.NewGuid().ToString("N")}{ext}";
			string fullPath = Path.Combine(path, newFileName);
			file.SaveFile(fullPath);
			return newFileName;
		}
	}
}
