using NSI.Common.Models;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.Membership
{
    public interface IUserManipulation
    {
        ICollection<UserDomain> GetAllUsers(int tenantId, string searchTerm);
        int AddUser(UserDomain user);
        UserDomain GetUserById(int id);
        UserDomain GetUserByIdentifier(Guid identifier);
        UserDomain GetUserByEmail(string email);
        ICollection<UserDomain> GetUsersByTenantId(int tenantId);
        void UpdateUser(UserDomain user);
        ICollection<UserDomain> SearchUsers(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
    }
}
