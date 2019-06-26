using NSI.Common.Models;
using NSI.Domain.Membership;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces
{
    public interface IUserRepository
    {
        ICollection<UserDomain> GetAll(int tenantId, string searchTerm);
        ICollection<UserDomain> GetUsersByTenantId(int tenantId);
        UserDomain GetUserById(int userId);
        UserDomain GetUserByEmail(string email);
        UserDomain GetUserByIdentifier(Guid identifier);
        bool IsEmailUnique(string email);
        int AddUser(UserDomain user);
        void UpdateUser(UserDomain user);
        ICollection<UserDomain> SearchUsers(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
        IList<UserInfo> GetUsersWithReferences(string searchTerm, params Expression<Func<UserInfo, object>>[] includes);
    }
}
