using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extentions.Common
{
    public static class DateTimeExtentions
    {
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            return new DateTimeExtensionHelper().GetCalculateAge(dateOfBirth);
        }


    }
}
