using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks.DocumentManagement;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.DocumentRepository.Implementations;
using NSI.Domain.DocumentManagement;
using NSI.Repository.Interfaces.DocumentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Tests.Business.DocumentManagement
{
    [TestClass]
    public class StorageTypeTests
    {
        private Mock<IStorageTypeRepository> _storageTypeRepositoryMock;
        private StorageTypeManipulation _storageTypeManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _storageTypeRepositoryMock = StorageTypeRepositoryMock.GetStorageTypeRepositoryMock();
            _storageTypeManipulation = new StorageTypeManipulation(
                _storageTypeRepositoryMock.Object
                );
        }

        #region Get Storage Type By ID - Tests
        [TestMethod, TestCategory("Storage Types - Get Storage Type By Id")]
        public void GetStorageTypeById_Success()
        {
            StorageTypeDomain storageType = _storageTypeManipulation.GetStorageTypeById(1);
            Assert.AreEqual(1, storageType.StorageTypeId);
            Assert.AreEqual(true, storageType.IsActive);
            Assert.AreEqual("azure", storageType.Name);
            Assert.AreEqual("azure", storageType.Code);
            Assert.AreEqual(0, storageType.Documents.Count);
        }

        [TestMethod, TestCategory("Storage Typess - Get Storage Type By Id")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetStorageTypeById_Fail_InvalidStorageTypeId()
        {
            _storageTypeManipulation.GetStorageTypeById(-1);
        }
        #endregion

        #region Get All Storage Types - Tests
        [TestMethod, TestCategory("Storage Types - Get All Storage Types")]
        public void GetAllStorageTypes_Success()
        {
            _storageTypeManipulation.GetAllStorageTypes();
        }
        #endregion

        #region Create Storage Type - Tests
        [TestMethod, TestCategory("Storage Types - Create Storage Type")]
        public void CreateStorageType_Success()
        {
            int storageTypeId = _storageTypeManipulation.CreateStorageType(GetValidStorageTypeDomain());
            Assert.AreEqual(1, storageTypeId);
        }
        #endregion

        #region Create Storage Type - Tests
        [TestMethod, TestCategory("Storage Types - Create Storage Type")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void CreateStorageType_Fail()
        {
            int storageTypeId = _storageTypeManipulation.CreateStorageType(null);
        }
        #endregion

        #region Update Storage Type - Tests
        [TestMethod, TestCategory("Storage Types - Update Storage Type")]
        public void UpdateStorageType_Success()
        {
            int storageTypeId = _storageTypeManipulation.UpdateStorageType(GetValidStorageTypeDomain());
            Assert.AreEqual(1, storageTypeId);
        }

        [TestMethod, TestCategory("Storage Types - Update Storage Type")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void UpdateStorageType_Fail_InvalidStorageType()
        {
            _storageTypeManipulation.UpdateStorageType(null);
        }
        #endregion

        #region Delete Storage Type - Tests
        [TestMethod, TestCategory("Storage Types - Delete Storage Type")]
        public void DeleteStorageType_Success()
        {
            bool isDeleted = _storageTypeManipulation.DeleteStorageType(1);
            Assert.AreEqual(true, isDeleted);
        }

        [TestMethod, TestCategory("Storage Types - Delete Storage Type")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void DeleteStorageType_Fail_InvalidStorageTypeId()
        {
            _storageTypeManipulation.DeleteStorageType(-1);
        }
        #endregion

        #region Valid Storage Type Models
        private StorageTypeDomain GetValidStorageTypeDomain()
        {
            return new StorageTypeDomain()
            {
                StorageTypeId = 1,
                IsActive = true,
                Name = "azure",
                Code = "azure",
                Documents = new List<DocumentDomain>()
            };
        }
        #endregion
    }

}
