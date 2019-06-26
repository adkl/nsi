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
    public interface ITemplateManipulation
    {
        ICollection<TemplateDomain> GetAll(IList<FilterCriteria> filterCriteria,
            IList<SortCriteria> sortCriteria, Paging paging);
        ICollection<TemplateDomain> GetAllByFolderId(int folderId, Paging paging);
        ICollection<TemplateDomain> GetAllByName(string name, Paging paging);
        TemplateDomain GetById(int templateId);
        int Add(CreateTemplateRequest template);
        string GetTemplateNameById(int templateId);
        bool Exists(int templateId);
        object DeleteByTemplateVersionId(int templateVersionId);
    }
}
