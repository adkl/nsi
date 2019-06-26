using NSI.DataContracts.Base;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Membership.Roles
{
    public class UpdateRoleRequest : BaseRequest
    {
        /// <summary>
        /// Permission model
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int ManipulationLogId { get; set; }
    }
}
