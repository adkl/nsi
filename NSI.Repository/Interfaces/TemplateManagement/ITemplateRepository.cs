using NSI.Common.Models;
using NSI.Domain.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.TemplateManagement
{
    public interface ITemplateRepository
    {
        ICollection<TemplateDomain> filter(IList<FilterCriteria> filterCriteria,
            IList<SortCriteria> sortCriteria, Paging paging);
        int Add(TemplateDomain template);
        bool Exists(int templateId);
        void DeleteByTemplateVersion(int templateVersionId);
    }
}
