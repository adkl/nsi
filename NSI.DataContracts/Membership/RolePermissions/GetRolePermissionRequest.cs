using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Membership.RolePermissions
{
    public class GetRolePermissionRequest : BaseRequest
    {
        /// <summary>
        /// Role Permission ID for retrieval
        /// </summary>
        public int Id { get; set; }
    }
}
