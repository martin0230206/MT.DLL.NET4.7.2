namespace MT.Utilities.UploadFile
{
	public class ValidationResult
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public static ValidationResult Ok()
		{
			return new ValidationResult { Success = true };
		}
		public static ValidationResult Fail(string message)
		{
			return new ValidationResult
			{
				Success = false,
				Message = message
			};
		}
	}
}
