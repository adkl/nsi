using NSI.Common.Models;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.Membership
{
    /// <summary>
    /// Exposes methods for tenants  manipulation
    /// </summary>
    public interface ITenantRepository
    {
        TenantDomain GetById(int tenantId);
        TenantDomain GetByIdentifier(Guid identifier);
        ICollection<TenantDomain> GetAll();
        int Add(TenantDomain tenant);
        void Update(TenantDomain tenant);
        ICollection<TenantDomain> SearchTenants(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
    }
}
