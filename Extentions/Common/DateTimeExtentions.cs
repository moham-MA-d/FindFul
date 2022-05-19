using System;

namespace Extensions.Common
{
    public static class DateTimeExtentions
    {
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            return new DateTimeExtensionHelper().GetCalculateAge(dateOfBirth);
        }


    }
}
