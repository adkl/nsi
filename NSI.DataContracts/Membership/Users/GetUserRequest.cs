using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Membership.Users
{
    public class GetUserRequest : BaseRequest
    {
        /// <summary>
        /// User ID for retrieval
        /// </summary>
        public int Id { get; set; }
    }
}
