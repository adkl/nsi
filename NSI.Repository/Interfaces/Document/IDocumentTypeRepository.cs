using NSI.Domain.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.Document
{
    public interface IDocumentTypeRepository
    {
        int Add(DocumentTypeDomain documentType);
        ICollection<DocumentTypeDomain> GetAll();
        DocumentTypeDomain GetById(int documentTypeId);
        DocumentTypeDomain GetByName(string documentTypeName);
    }
}
