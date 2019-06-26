using NSI.DataContracts.Base;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Membership.RolePermissions
{
    public class UpdateRolePermissionRequest : BaseRequest
    {
        /// <summary>
        /// Role Permission model
        /// </summary>
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int TenantId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

    }
}
