using NSI.Domain.DocumentManagement;
using NSI.EF;

namespace NSI.Repository.Extensions
{
    public static class DocumentExtension
    {
        public static DocumentDomain ToDomainModel(this Document obj)
        {
            return obj == null ? null : new DocumentDomain()
            {
                DocumentId = obj.DocumentId,
                Name = obj.Name,
                Path = obj.Path,
                FileSize = obj.FileSize,
                ExternalId = obj.ExternalId,
                LocationExternalId = obj.LocationExternalId,
                DateCreated = obj.DateCreated,
                StorageTypeId = obj.StorageTypeId,
                FileTypeId =  (int)obj.FileTypeId,
                Description = obj.Description
        };
        }

        public static Document FromDomainModel(this Document obj, DocumentDomain domain)
        {
            if (obj == null)
            {
                obj = new Document();
            }

            obj.DocumentId = domain.DocumentId;
            obj.Name = domain.Name;
            obj.Path = domain.Path;
            obj.FileSize = domain.FileSize;
            obj.ExternalId = domain.ExternalId;
            obj.LocationExternalId = domain.LocationExternalId;
            obj.DateCreated = domain.DateCreated;
            obj.StorageTypeId = domain.StorageTypeId;
            obj.FileTypeId = domain.FileTypeId;
            obj.Description = domain.Description;
            return obj;

        }
    }
}
