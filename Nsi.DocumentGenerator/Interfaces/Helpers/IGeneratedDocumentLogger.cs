using NSI.Common.Models;
using NSI.DataContracts.Document;
using NSI.Domain.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DocumentGenerator.Interfaces.Helpers
{
    public interface IGeneratedDocumentLogger
    {
        void Log(GeneratedDocumentDomain generatedDocument);
        ICollection<GeneratedDocumentDomain> GetAllLogs(IList<FilterCriteria> filterCriteria,
            IList<SortCriteria> sortCriteria, Paging paging);
    }
}
