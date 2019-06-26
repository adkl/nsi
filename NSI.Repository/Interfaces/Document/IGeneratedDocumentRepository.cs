using NSI.Common.Models;
using NSI.Domain.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.Document
{
    public interface IGeneratedDocumentRepository
    {
        int Add(GeneratedDocumentDomain generatedDocument);
        ICollection<GeneratedDocumentDomain> GetAll(IList<FilterCriteria> filterCriteria,
            IList<SortCriteria> sortCriteria, Paging paging);
        GeneratedDocumentDomain GetById(int generatedDocumentId);
    }
}
