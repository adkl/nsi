using NSI.Common.Models;
using NSI.DataContracts.TemplateManagement;
using NSI.Domain.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.TemplateManagement
{
    public interface ITemplateVersionManipulation
    {
        ICollection<TemplateVersionDomain> GetAllByTemplateId(int templateId, Paging paging);
        TemplateVersionDomain GetByVersionId(int templateVersionId);
        TemplateVersionDomain GetDefaultByTemplateId(int templateId);
        int Add(TemplateVersionDomain templateVersion);
        int Add(CreateTemplateVersionRequest templateVersion);
        object DeleteByVersionId(int templateVersionId);
        ICollection<TemplateVersionDomain> GetAll(IList<FilterCriteria> filterCriteria, 
            IList<SortCriteria> sortCriteria, Paging paging);
    }
}
