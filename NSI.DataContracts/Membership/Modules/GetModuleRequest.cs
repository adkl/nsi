using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Membership.Modules
{
    public class GetModuleRequest : BaseRequest
    {
        /// <summary>
        /// Module ID for retrieval
        /// </summary>
        public int Id { get; set; }
    }
}