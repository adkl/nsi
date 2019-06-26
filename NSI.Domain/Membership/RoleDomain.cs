using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NSI.Domain.Membership
{
    public class RoleDomain : BaseDomain
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int ManipulationLogId { get; set; }
        public IList<RolePermissionDomain> RolePermission { get; set; }
        //public int TenantId { get; set; } BaseDomain containts TenantdId
 
    }
}
