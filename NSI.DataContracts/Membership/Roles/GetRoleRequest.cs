using NSI.Common.Enumerations;
using NSI.DataContracts.Base;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Membership.Roles
{
    public class GetRoleRequest : BaseRequest
    {
        /// <summary>
        /// Role ID for retrieval
        /// </summary>
        public int Id { get; set; }
    }
}
