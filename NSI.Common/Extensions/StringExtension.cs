using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Common.Extensions
{
    public static class StringExtension
    {
        public static String SafeTrim(this String value)
        {
            return !string.IsNullOrEmpty(value) ? value.Trim() : null;
        }
    }
}
