using System;

namespace Extensions.Common
{
    public class DateTimeExtensionHelper
    {
        public int GetCalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) --age;

            return age;
        }
    }
}
