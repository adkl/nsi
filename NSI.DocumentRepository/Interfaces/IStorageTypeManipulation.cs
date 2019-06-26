using NSI.Domain.DocumentManagement;
using System.Collections.Generic;

namespace NSI.DocumentRepository.Interfaces
{
    public interface IStorageTypeManipulation
    {
        ICollection<StorageTypeDomain> GetAllStorageTypes();
        StorageTypeDomain GetStorageTypeById(int id);
        int CreateStorageType(StorageTypeDomain storageType);
        int UpdateStorageType(StorageTypeDomain storageType);
        bool DeleteStorageType(int storageTypeId);
    }
}
