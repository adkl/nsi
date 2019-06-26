
using NSI.Common.Exceptions;
using NSI.Common.Resources.DocumentManagement;
using NSI.DocumentRepository.Interfaces;
using NSI.Domain.DocumentManagement;
using NSI.Repository.Interfaces.DocumentManagement;
using System.Collections.Generic;

namespace NSI.DocumentRepository.Implementations
{
    public class StorageTypeManipulation : IStorageTypeManipulation
    {
        private readonly IStorageTypeRepository _storageTypeRepository;

        public StorageTypeManipulation(IStorageTypeRepository storageTypeRepository)
        {
            _storageTypeRepository = storageTypeRepository;
        }

        public ICollection<StorageTypeDomain> GetAllStorageTypes()
        { 
            return _storageTypeRepository.GetAllStorageTypes();
        }

        public StorageTypeDomain GetStorageTypeById(int id)
        {
            if (id <= 0) throw new NsiArgumentException(StorageTypeMessages.StorageTypeInvalidArgument);

            return _storageTypeRepository.GetStorageTypeById(id);
        }

        public int CreateStorageType(StorageTypeDomain storageType)
        {
            if (storageType == null) throw new NsiArgumentException(StorageTypeMessages.StorageTypeInvalidArgument);

            return _storageTypeRepository.CreateStorageType(storageType);
        }

        public int UpdateStorageType(StorageTypeDomain storageType)
        {
            if (storageType == null) throw new NsiArgumentException(StorageTypeMessages.StorageTypeInvalidArgument);

            return _storageTypeRepository.UpdateStorageType(storageType);
        }

        public bool DeleteStorageType(int storageTypeId)
        {
            if (storageTypeId <= 0) throw new NsiArgumentException(StorageTypeMessages.StorageTypeInvalidArgument);

            return _storageTypeRepository.DeleteStorageType(storageTypeId);
        }
    }
}
