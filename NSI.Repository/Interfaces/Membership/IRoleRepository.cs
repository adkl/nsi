using NSI.Common.Models;
using NSI.Domain.Membership;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.Membership
{
   public interface IRoleRepository
    {
      

        ICollection<RoleDomain> GetAll(int tenantId, string searchTerm);
        RoleDomain GetById(int roleId);
        RoleDomain GetByName(string roleName);
        int Add(RoleDomain role);
        void Update(RoleDomain role);
        ICollection<RoleDomain> SearchRoles(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
        IList<Role> GetRolesWithReferences(string searchTerm, params Expression<Func<Role, object>>[] includes);


    }
}
