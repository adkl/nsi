using NSI.Common.Models;
using NSI.DocumentGenerator.Interfaces.Helpers;
using NSI.Domain.Document;
using NSI.Repository.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DocumentGenerator.Implementations.Helpers
{
    public class GeneratedDocumentLogger : IGeneratedDocumentLogger
    {
        private readonly IGeneratedDocumentRepository _generatedDocumentRepository;

        public GeneratedDocumentLogger(
            IGeneratedDocumentRepository generatedDocumentRepository
            )
        {
            _generatedDocumentRepository = generatedDocumentRepository;
        }

        public ICollection<GeneratedDocumentDomain> GetAllLogs(IList<FilterCriteria> filterCriteria,
            IList<SortCriteria> sortCriteria, Paging paging)
        {
            return _generatedDocumentRepository.GetAll(filterCriteria, sortCriteria, paging);
        }

        public void Log(GeneratedDocumentDomain generatedDocument)
        {
            _generatedDocumentRepository.Add(generatedDocument);
        }
    }
}
