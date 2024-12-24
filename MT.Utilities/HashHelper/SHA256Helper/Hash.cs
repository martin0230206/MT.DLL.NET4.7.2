using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MT.Utilities.HashHelper.SHA256Helper
{
	public class Hash
	{
		public static string ToSHA256(string plainText, string salt)
		{
			using (var sha256 = SHA256.Create())
			{
				var passwordBytes = Encoding.UTF8.GetBytes(plainText + salt);
				var hashBytes = sha256.ComputeHash(passwordBytes);
				var sb = new StringBuilder();
				foreach (var hashByte in hashBytes)
				{
					sb.Append(hashByte.ToString("X2"));
				}
				return sb.ToString();
			}
		}
		public static string GetSalt()
		{
			return System.Configuration.ConfigurationManager.AppSettings["Salt"];
		}
		public static bool Verify(string plainText, string encryptedText)
		{
			var hashText = ToSHA256(plainText, GetSalt());
			var result = hashText == encryptedText;
			return result;
		}
	}
}
