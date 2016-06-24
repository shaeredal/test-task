﻿using System.Text.RegularExpressions;

namespace OnlinerNotifier.BLL.Validators
{
    public class EmailValidator
    {
        private string emailRegexString =
          @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        public bool IsValid(string email)
        {
            return Regex.IsMatch(email, emailRegexString, RegexOptions.IgnoreCase);
        }
    }
}
