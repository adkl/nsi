using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.Base
{
    public class BaseAuditDomain : BaseDomain
    {
        public AuditTrailDomain AuditTrail { get; set; }
    }
}
