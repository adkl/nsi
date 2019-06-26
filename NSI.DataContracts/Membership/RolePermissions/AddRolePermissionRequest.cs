using NSI.DataContracts.Base;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Membership.RolePermissions
{
    public class AddRolePermissionRequest : BaseRequest
    {
        /// <summary>
        /// Permission model
        /// </summary>
        public string ID { get; set; }
        public bool IsActive { get; set; }
        public int TenantId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

    }
}
