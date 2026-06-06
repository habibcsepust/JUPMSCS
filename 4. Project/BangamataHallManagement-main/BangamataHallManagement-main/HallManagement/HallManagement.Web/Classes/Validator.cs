using HallManagement.Model.Entities;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HallManagement.Web.Classes
{
    public static class Validator
    {
        public static bool IsValidPhoneOrEmail(string emailOrPhone, out string error, out bool? isEmail)
        {
            error = string.Empty;
            isEmail = null;
            bool isValid = true;
            if (string.IsNullOrEmpty(emailOrPhone))
            {
                isValid = false;
                error = "Please enter email or phone number.";
                return isValid;
            }

            if (IsNumeric(emailOrPhone))
            {
                if (!IsValidPhoneNumber(emailOrPhone))
                {
                    isValid = false;
                    error = "Please enter a valid phone number";
                }
                else
                {
                    isEmail = false;
                }
            }
            else if (emailOrPhone.Contains("@"))
            {
                if (!IsValidEmail(emailOrPhone))
                {
                    isValid = false;
                    error = "Please enter a valid email";
                }
                else
                {
                    isEmail = true;
                }
            }
            else
            {
                isValid = false;
                error = "Enter a valid Email or Phone number";
            }
            return isValid;
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^01(3|4|5|6|7|8|9)\d{8}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        public static bool IsNumeric(string input)
        {
            string numericPattern = @"^\d+$";
            Regex regex = new Regex(numericPattern);
            return regex.IsMatch(input);
        }

        public static DateTime ConvertOtpSentDate(string dateString)
        {
            if (DateTime.TryParseExact(dateString, "MM/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                return result;
            return DateTime.Now;
        }
    }
}
