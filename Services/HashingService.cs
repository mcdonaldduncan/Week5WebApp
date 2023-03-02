using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Week5WebApp.Services
{
    public class HashingService
    {
        public string HashString(string userEnteredPassword)
        {
            using (SHA256 hasher = SHA256.Create())
            {
                var bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(userEnteredPassword));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();

            }
        }

        public bool CompareHash(string userEnteredPassword, string storedPassword)
        {
            return HashString(userEnteredPassword) == storedPassword;
        }
    }
}