using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extentions.Common
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string input)
        {
            if (string.IsNullOrEmpty(input) && string.IsNullOrWhiteSpace(input))
                return true;
            
            return false;
        }
    }
}
