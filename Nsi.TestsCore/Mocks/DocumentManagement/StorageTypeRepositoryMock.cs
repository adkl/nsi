using Moq;
using NSI.Domain.DocumentManagement;
using NSI.Repository.Interfaces.DocumentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks.DocumentManagement
{
    public class StorageTypeRepositoryMock
    {
        public static Mock<IStorageTypeRepository> GetStorageTypeRepositoryMock()
        {
            var storageTypeRepository = new Mock<IStorageTypeRepository> { CallBase = false };

            #region Get Storage Type By ID
            storageTypeRepository.Setup(x => x.GetStorageTypeById(1)).Returns(
                new StorageTypeDomain()
                {
                    StorageTypeId = 1,
                    IsActive = true,
                    Name = "azure",
                    Code = "azure",
                    Documents = new List<DocumentDomain>()
                });
            #endregion

            #region Get all Storage Types
            storageTypeRepository.Setup(x => x.GetAllStorageTypes()).Returns(new List<StorageTypeDomain>() {
                new StorageTypeDomain()
                {
                    StorageTypeId = 1,
                    IsActive = true,
                    Name = "azure",
                    Code = "azure",
                    Documents = new List<DocumentDomain>()
                },
                new StorageTypeDomain()
                {
                    StorageTypeId = 2,
                    IsActive = false,
                    Name = "test",
                    Code = "test",
                    Documents = new List<DocumentDomain>()
                }
            });

            #endregion

            #region Create Storage Type
            storageTypeRepository.Setup(x => x.CreateStorageType(It.IsAny<StorageTypeDomain>()))
                .Returns(1);
            #endregion

            #region Update Storage Type
            storageTypeRepository.Setup(x => x.UpdateStorageType(It.IsAny<StorageTypeDomain>()))
                .Returns(1);
            #endregion

            #region Delete Storage Type
            storageTypeRepository.Setup(x => x.DeleteStorageType(1))
                .Returns(true);
            #endregion

            return storageTypeRepository;
        }

    }
}
