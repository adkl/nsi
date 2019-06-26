using NSI.Common.Models;
using NSI.Domain.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.TemplateManagement
{
    public interface ITemplateVersionRepository
    {
        ICollection<TemplateVersionDomain> filter(IList<FilterCriteria> filterCriteria,
            IList<SortCriteria> sortCriteria, Paging paging);
        int Add(TemplateVersionDomain templateVersion);
        void UpdateTemplateVersion(TemplateVersionDomain templateVersion);
        void DeleteTemplateVersion(int templateVersionId);
    }
}
