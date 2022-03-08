using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WGU_Xamarin
{
    public static class Validation
    {
        public static bool ValidateName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }
        public static bool ValidatePhone(string phone)
        {
            string phoneMap = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";            

            if (!string.IsNullOrEmpty(phone))
                return Regex.IsMatch(phone, phoneMap);
            return false;
        }
        public static bool ValidateEmail(string email)
        {
            string emailMap = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";

            if (!string.IsNullOrEmpty(email))
                return Regex.IsMatch(email, emailMap);
            return false;
        }
    }
}
