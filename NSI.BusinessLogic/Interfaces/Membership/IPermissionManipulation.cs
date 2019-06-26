using NSI.Common.Models;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.Membership
{
    /// <summary>
    /// Exposes methods for manipulating permissions
    /// </summary>
    public interface IPermissionManipulation
    {
        ICollection<PermissionDomain> GetAllPermissions(string searchTerm);
        PermissionDomain GetPermissionById(int permissionId);
        PermissionDomain GetPermissionByCode(string permissionCode);
        int AddPermission(PermissionDomain permission);
        void UpdatePermission(PermissionDomain permission);
        ICollection<PermissionDomain> SearchPermissions(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
        
       
    }
}
