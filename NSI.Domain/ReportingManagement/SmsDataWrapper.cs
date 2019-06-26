using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.ReportingManagement
{
    public class SmsDataWrapper
    {
        public int SmsCount { get; set; }
        public List<Sms> Sms { get; set; }
    }
}
