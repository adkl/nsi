using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.Membership
{
    public class RolePermissionDomain : BaseDomain
    {
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public String Code { get; set; }
    }
}
