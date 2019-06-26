using NSI.Common.Models;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.Membership
{
    public interface IModuleRepository
    {
        ICollection<ModuleDomain> GetAll();
        ModuleDomain GetById(int moduleId);
        ModuleDomain GetByCode(string moduleCode);
        int Add(ModuleDomain module);
        void Update(ModuleDomain module);
        ICollection<ModuleDomain> GetTenantModules(int tenantId);
        ICollection<ModuleDomain> SearchModules(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
    }
}
