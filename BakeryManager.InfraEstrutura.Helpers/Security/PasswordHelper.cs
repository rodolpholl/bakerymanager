using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BakeryManager.Infraestrutura.Helpers.Security
{
    public static class PasswordHelper
    {

        public static bool ValidatePasswordOfDay(string mask, string input)
        {

            mask = mask.ToUpperInvariant();
            var now = DateTime.Now;

            if (mask.Contains("#Y#"))
                mask = mask.Replace("#Y#", now.Year.ToString());

            if (mask.Contains("#M#"))
                mask = mask.Replace("#M#", now.Date.ToString("MM"));

            if (mask.Contains("#D#"))
                mask = mask.Replace("#D#", now.Date.ToString("dd"));

            if (mask.Contains("#H#"))
                mask = mask.Replace("#H#", now.Date.ToString("HH"));
            
            return input.Equals(mask);

        }

        public static string CryptoSHA512(string TextToCryptograph)
        {
            var sha512 = new HMACSHA512();
            byte[] passwordArray = System.Text.Encoding.Default.GetBytes(TextToCryptograph);

            return Convert.ToBase64String(sha512.ComputeHash(passwordArray));
        }

        public static string CryptoSHA256(string TextToCryptograph)
        {
            var sha256 = new HMACSHA256();
            byte[] passwordArray = System.Text.Encoding.Default.GetBytes(TextToCryptograph);

            return Convert.ToBase64String(sha256.ComputeHash(passwordArray));
        }

        public static string CryptoMD5(string TextToCryptograph)
        {
            var md5 = new HMACMD5();
            byte[] passwordArray = System.Text.Encoding.Default.GetBytes(TextToCryptograph);

            return Convert.ToBase64String(md5.ComputeHash(passwordArray));
        }

        public static bool ValidatePasswordFormat(string password)
        {
            const int MIN_LENGTH = 6;
            const int MAX_LENGTH = 15;

            if (password == null) throw new ArgumentNullException();

            bool meetsLengthRequirements = password.Length >= MIN_LENGTH && password.Length <= MAX_LENGTH;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;

            if (meetsLengthRequirements)
            {
                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                }
            }

            return meetsLengthRequirements
                   && hasUpperCaseLetter
                   && hasLowerCaseLetter
                   && hasDecimalDigit;
                        
        }

    }
}
