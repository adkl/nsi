using NSI.Domain.Document;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class GeneratedDocumentExtension
    {
        public static GeneratedDocumentDomain ToDomainModel(this GeneratedDocument obj)
        {
            return obj == null ? null : new GeneratedDocumentDomain()
            {
                Id = obj.GeneratedDocumentId,
                Name = obj.Name,
                ExternalId = obj.ExternalId,
                Success = obj.Success,
                DateCreated = obj.DateCreated,
                Content = null,
                TemplateVersionId = obj.TemplateVersionId,
                DocumentType = obj.DocumentType.ToDomainModel()
            };
        }

        public static GeneratedDocument FromDomainModel(this GeneratedDocument obj, GeneratedDocumentDomain domain)
        {
            if (obj == null)
            {
                obj = new GeneratedDocument();
            }

            obj.GeneratedDocumentId = domain.Id;
            obj.Name = domain.Name;
            obj.ExternalId = domain.ExternalId;
            obj.Success = domain.Success;
            obj.DateCreated = domain.DateCreated;
            obj.Content = domain.Content;
            obj.TemplateVersionId = domain.TemplateVersionId;
            obj.DocumentTypeId = domain.DocumentTypeId;

            return obj;
        }
    }
}
