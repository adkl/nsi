using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetUtcDateTime()
        {
            return DateTime.UtcNow;
        }
    }
}
