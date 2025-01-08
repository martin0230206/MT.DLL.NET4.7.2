using MT.Utilities.UploadFile.Interfaces;

namespace MT.Utilities.UploadFile.Extensions
{
	public static class IMyFileExtensions
	{
		public static bool HasFile(this IMyFile file)
		{
			return !(
				file == null ||
				file.ContentLength == 0 ||
				string.IsNullOrEmpty(file.FileName)
			);
		}
	}
}
