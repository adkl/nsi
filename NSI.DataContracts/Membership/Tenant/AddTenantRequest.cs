using NSI.DataContracts.Base;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Membership.Tenant
{
    public class AddTenantRequest : BaseRequest
    {
        /// <summary>
        /// Tenant model
        /// </summary>
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int DefaultLanguageId { get; set; }
    }
}
