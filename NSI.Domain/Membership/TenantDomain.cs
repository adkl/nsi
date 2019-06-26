using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.Membership
{
    public class TenantDomain : BaseDomain
    {
        public Guid Identifier { get; set; }
        public string Name { get; set; }
        public int DefaultLanguageId { get; set; }
        public bool IsActive { get; set; }
    }
}
