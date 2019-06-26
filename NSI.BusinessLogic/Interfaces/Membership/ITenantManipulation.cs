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
    /// Exposes methods for manipulating tenants
    /// </summary>
    public interface ITenantManipulation
    {
        TenantDomain GetTenantById(int tenantId);
        TenantDomain GetTenantByIdentifier(Guid identifier);
        ICollection<TenantDomain> GetAllTenants();
        int AddTenant(TenantDomain tenant);
        void UpdateTenant(TenantDomain tenant);
        ICollection<TenantDomain> SearchTenants(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
    }
}
