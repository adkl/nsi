using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.ReportingManagement
{
    public class Sms
    {
        public string PhoneNumber { get; set; }
        public string From { get; set; }
        public string Content { get; set; }
    }
}
