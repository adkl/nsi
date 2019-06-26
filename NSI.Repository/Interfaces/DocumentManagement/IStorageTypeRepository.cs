using NSI.Domain.DocumentManagement;
using System.Collections.Generic;

namespace NSI.Repository.Interfaces.DocumentManagement
{
    public interface IStorageTypeRepository
    {
        ICollection<StorageTypeDomain> GetAllStorageTypes();
        StorageTypeDomain GetStorageTypeById(int id);
        int CreateStorageType(StorageTypeDomain storageType);
        int UpdateStorageType(StorageTypeDomain storageType);
        bool DeleteStorageType(int storageTypeId);
    }
}
