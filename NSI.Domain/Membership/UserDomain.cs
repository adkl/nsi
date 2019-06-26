using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.Membership
{
    public class UserDomain : BaseDomain
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string TimeZoneId { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int LanguageId { get; set; }
        public Guid Identifier { get; set; }
        public IList<RoleMemberDomain> RoleMember { get; set; }
        
        // Computed properties
        public string FullName
        {
            get
            {
                return FirstName + " " +  (!string.IsNullOrWhiteSpace(MiddleName) ? (MiddleName + " ") : string.Empty) + LastName;
            }
        }
    }
}
