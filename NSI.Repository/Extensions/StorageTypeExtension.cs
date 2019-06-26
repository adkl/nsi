using NSI.Domain.DocumentManagement;
using NSI.EF;
using System.Linq;

namespace NSI.Repository.Extensions
{
    public static class StorageTypeExtension
    {
        public static StorageTypeDomain ToDomainModel(this StorageType obj)
        {
            return obj == null ? null : new StorageTypeDomain()
            {
                StorageTypeId = obj.StorageTypeId,
                Name = obj.Name,
                Code = obj.Code,
                IsActive = obj.IsActive,
                Documents = obj.Document.Select(x => x.ToDomainModel()).ToList(),
            };
        }

        public static StorageType FromDomainModel(this StorageType obj, StorageTypeDomain domain)
        {
            if (obj == null)
            {
                obj = new StorageType();
            }

            obj.StorageTypeId = domain.StorageTypeId;
            obj.Name = domain.Name;
            obj.Code = domain.Code;
            obj.IsActive = domain.IsActive;

            return obj;

        }
    }
}
