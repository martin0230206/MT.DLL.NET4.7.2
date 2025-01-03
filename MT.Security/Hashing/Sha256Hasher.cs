using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MT.Security.Hashing
{
    public class Sha256Hasher
    {
        /// <summary>
        /// 將明文與鹽值進行 SHA256 雜湊運算。
        /// </summary>
        /// <param name="plainText">明文。</param>
        /// <param name="salt">鹽值。</param>
        /// <returns>雜湊後的字串。</returns>
        public static string ComputeHash(string plainText, string salt)
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

        /// <summary>
        /// 獲取設定中的鹽值。
        /// </summary>
        /// <returns>鹽值字串。</returns>
        public static string GetSalt()
        {
            return System.Configuration.ConfigurationManager.AppSettings["Salt"];
        }

        /// <summary>
        /// 驗證明文與加密後的字串是否相符。
        /// </summary>
        /// <param name="plainText">明文。</param>
        /// <param name="encryptedText">加密後的字串。</param>
        /// <returns>是否相符。</returns>
        public static bool Verify(string plainText, string encryptedText)
        {
            var hashText = ComputeHash(plainText, GetSalt());
            return hashText == encryptedText;
        }
    }
}
