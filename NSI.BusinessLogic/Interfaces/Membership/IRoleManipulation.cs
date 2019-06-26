using NSI.Common.Models;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.Membership
{
   public interface IRoleManipulation
    {
        ICollection<RoleDomain> GetAllRoles(int tenantId,string searchTerm);
        RoleDomain GetRoleById(int roleId);
        RoleDomain GetRoleByName(string name);
        int AddRole(RoleDomain role);
        void UpdateRole(RoleDomain role);
        ICollection<RoleDomain> SearchRoles(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);

    }
}
