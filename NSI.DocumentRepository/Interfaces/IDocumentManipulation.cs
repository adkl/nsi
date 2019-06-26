
using NSI.Common.Models;
using NSI.Domain.DocumentManagement;
using System.Collections.Generic;

namespace NSI.DocumentRepository.Interfaces
{
    public interface IDocumentManipulation
    {
        ICollection<DocumentDomain> GetAllDocuments();
        DocumentDomain GetDocumentById(int id, int storageTypeId);
        IEnumerable<DocumentDomain> Search(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
        int CreateDocument(DocumentDomain document);
        int UpdateDocument(DocumentDomain document);
        bool DeleteDocument(int documentId);
        ICollection<DocumentDomain> GetAllDocumentsWithStorageTypeId(int id);
    }
}
