using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Business.Common
{
    public static class StringExtensions
    {
        public static string NormalizeEmail(string email)
        {
            string result;
            string[] emailParts = email.Split('@', StringSplitOptions.RemoveEmptyEntries);

            emailParts[0] = emailParts[0].Replace(".", "");

            int atIndex = emailParts[0].IndexOf("+", StringComparison.Ordinal);
            if (atIndex != -1)
                emailParts[0] = emailParts[0].Remove(atIndex);

            result = string.Join("@", emailParts);
            return result;
        }
    }
}
