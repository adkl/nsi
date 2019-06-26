using NSI.Common.Models;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.Membership
{
    public interface IModuleManipulation
    {
        ICollection<ModuleDomain> GetAllModules();
        ModuleDomain GetModuleById(int moduleId);
        int AddModule(ModuleDomain module);
        void UpdateModule(ModuleDomain module);
        ICollection<ModuleDomain> SearchModules(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
    }
}
