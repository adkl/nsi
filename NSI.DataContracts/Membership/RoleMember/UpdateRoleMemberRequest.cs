using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Membership.RoleMember
{
    public class UpdateRoleMemberRequest : BaseRequest
    {
        /// <summary>
        /// Role member update request model
        /// </summary>
        /// 
        public int Id { get; set; }
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

    }
}
