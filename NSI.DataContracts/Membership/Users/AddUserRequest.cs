using NSI.DataContracts.Base;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Membership.Users
{
    public class AddUserRequest : BaseRequest
    {
        /// <summary>
        /// User add request model
        /// </summary>
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string TimeZoneId { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int LanguageId { get; set; }
        public int TenantId { get; set; }
    }
}
