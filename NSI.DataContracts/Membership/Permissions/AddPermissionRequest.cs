using NSI.DataContracts.Base;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Membership.Permissions
{
    public class AddPermissionRequest : BaseRequest
    {
        /// <summary>
        /// Permission model
        /// </summary>
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public int ModuleId { get; set; }
    }
}
