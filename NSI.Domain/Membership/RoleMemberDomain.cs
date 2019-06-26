using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.Membership
{
    public class RoleMemberDomain : BaseAuditDomain
    {
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public int UserTenantId { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
    }
}
