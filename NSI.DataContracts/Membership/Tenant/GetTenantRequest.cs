using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Membership.Tenant
{    
    public class GetTenantRequest : BaseRequest
    {
        /// <summary>
        /// Tenant ID for retrieval
        /// </summary>
        public int Id { get; set; }
    }
}
