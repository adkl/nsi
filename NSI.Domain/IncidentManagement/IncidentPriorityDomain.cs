using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.IncidentManagement
{
    public class IncidentPriorityDomain : BaseDomain
    {
        public int PriorityId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ColorCode { get; set; }
        public bool IsActive { get; set; }
        public string IconPath { get; set; }
    }
}
