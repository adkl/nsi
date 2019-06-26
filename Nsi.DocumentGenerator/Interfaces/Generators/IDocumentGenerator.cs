using NSI.Common.Enumerations;
using NSI.Domain.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DocumentGenerator.Interfaces.Generators
{
    public interface IDocumentGenerator
    {
        GeneratedDocumentDomain Generate(string inputContent, string fileName, DocumentTypeDomain inputDocType,
           DocumentTypeDomain outputDocTypeDomain, Nullable<int> templateVersionId);
        GeneratedDocumentDomain Generate(string inputContent, string fileName, DocumentTypeEnum inputDocType,
           DocumentTypeEnum outputDocTypeDomain,Nullable<int> templateVersionId);        
        // Temporary method for initial document types creation
        void AddBasicDocTypesToDb();
        // Temporary method for testing
        ICollection<DocumentTypeDomain> GetAllTypes();
        
    }
}
