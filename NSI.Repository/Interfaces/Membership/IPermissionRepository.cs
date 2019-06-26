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
    /// <summary>
    /// Exposes methods for permissions manipulation
    /// </summary>
    public interface IPermissionRepository
    {
        ICollection<PermissionDomain> GetAll(string searchTerm);
        PermissionDomain GetById(int permissionId);
        PermissionDomain GetByCode(string permissionCode);
        int Add(PermissionDomain permission);
        void Update(PermissionDomain permission);
        IList<Permission> GetPermissionsWithReferences(string searchTerm, params Expression<Func<Permission, object>>[] includes);
        ICollection<PermissionDomain> SearchPermissions(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
    }
}
