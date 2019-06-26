using NSI.Domain.Document;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class DocumentTypeExtension
    {
        public static DocumentTypeDomain ToDomainModel(this DocumentType obj)
        {
            return obj == null ? null : new DocumentTypeDomain()
            {
                Id = obj.DocumentTypeId,
                Name = obj.Name,
                Code = obj.Code,
                Encoding = obj.Encoding,    
                Version = obj.Version
            };
        }

        public static DocumentType FromDomainModel(this DocumentType obj, DocumentTypeDomain domain)
        {
            if (obj == null)
            {
                obj = new DocumentType();
            }

            obj.DocumentTypeId = domain.Id;
            obj.Name = domain.Name;
            obj.Code = domain.Code;
            obj.Encoding = domain.Encoding;
            obj.Version = domain.Version;

            return obj;
        }
    }
}
