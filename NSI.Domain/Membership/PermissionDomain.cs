using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.Membership
{
    public class PermissionDomain : BaseDomain
    {
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public int ModuleId { get; set; }
    }
}
