using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks;
using Nsi.TestsCore.Mocks.DeviceManagement;
using NSI.BusinessLogic.DeviceManagement;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Domain.DeviceManagement;
using NSI.Domain.IncidentManagement;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces.DeviceManagement;
using NSI.Repository.Interfaces.IncidentManagement;
using System;
using System.Collections.Generic;

namespace NSI.Tests.Business.DeviceMangement
{
    [TestClass]
    public class DeviceTypeManipulationTests
    {
        private Mock<IDeviceTypeRepository> _deviceTypeRepositoryMock;
        private DeviceTypeManipulation _deviceTypeManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _deviceTypeRepositoryMock = DeviceTypeRepositoryMock.GetDeviceTypeRepositoryMock();
            _deviceTypeManipulation = new DeviceTypeManipulation(
                _deviceTypeRepositoryMock.Object
                );
        }

        #region Get Device Type By ID - Tests
        [TestMethod, TestCategory("Device Types - Get Device Type By Id")]
        public void GetDeviceTypeById_Success()
        {
            DeviceTypeDomain deviceType = _deviceTypeManipulation.GetDeviceTypeById(1);
            Assert.AreEqual(1, deviceType.DeviceTypeId);
        }

        [TestMethod, TestCategory("Device Typess - Get Device Type By Id")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetDeviceTypeById_Fail_InvalidDeviceId()
        {
            _deviceTypeManipulation.GetDeviceTypeById(-1);
        }
        #endregion

        #region Get All Device Types - Tests
        [TestMethod, TestCategory("Device Types - Get All Device Types")]
        public void GetAllDeviceTypes_Success()
        {
            _deviceTypeManipulation.GetAllDeviceTypes();
        }
        #endregion

        #region Get All Active Device Types - Tests
        [TestMethod, TestCategory("Device Types - Get All Active Device Types")]
        public void GetAllActiveDeviceTypes_Success()
        {
            _deviceTypeManipulation.GetAllActiveDeviceTypes();
        }
        #endregion

        #region Get All Inactive Device Types - Tests
        [TestMethod, TestCategory("Device Types - Get All Inactive Device Types")]
        public void GetAllInactiveDeviceTypes_Success()
        {
            _deviceTypeManipulation.GetAllInactiveDeviceTypes();
        }
        #endregion

        #region Create Device Type - Tests
        [TestMethod, TestCategory("Device Types - Create Device Type")]
        public void CreateDeviceType_Success()
        {
            int deviceTypeId = _deviceTypeManipulation.CreateDeviceType(GetValidDeviceTypeDomain());
            Assert.AreEqual(1, deviceTypeId);
        }
        #endregion

        #region Update Device Type - Tests
        [TestMethod, TestCategory("Device Types - Update Device Type")]
        public void UpdateDeviceType_Success()
        {
            int deviceTypeId = _deviceTypeManipulation.UpdateDeviceType(GetValidDeviceTypeDomain());
            Assert.AreEqual(1, deviceTypeId);
        }

        [TestMethod, TestCategory("Device Types - Update Device Type")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void UpdateDeviceType_Fail_InvalidDeviceType()
        {
            _deviceTypeManipulation.UpdateDeviceType(null);
        }
        #endregion

        #region Delete Device Type - Tests
        [TestMethod, TestCategory("Device Types - Delete Device Type")]
        public void DeleteDeviceType_Success()
        {
            bool isDeleted = _deviceTypeManipulation.DeleteDeviceType(1);
            Assert.AreEqual(true, isDeleted);
        }

        [TestMethod, TestCategory("Device Types - Delete Device Type")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void DeleteDeviceType_Fail_InvalidDeviceTypeId()
        {
            _deviceTypeManipulation.DeleteDeviceType(-1);
        }
        #endregion

        #region Search Device Types - Tests
        [TestMethod, TestCategory("Device Types - Search Device Types")]
        public void SearchDeviceTypes_Success()
        {
            ICollection<DeviceTypeDomain> deviceTypes = _deviceTypeManipulation.SearchDeviceTypes("Device Type 2");

            Assert.AreEqual(1, deviceTypes.Count);
        }

        [TestMethod, TestCategory("Device Types - Search Device Types")]
        public void SearchDeviceTypesWithFilter_Success()
        {
            ICollection<DeviceTypeDomain> deviceTypes = _deviceTypeManipulation.SearchDeviceTypes("Device Type 2", true);

            Assert.AreEqual(1, deviceTypes.Count);
        }
        #endregion

        #region Valid Domain Models
        private DeviceTypeDomain GetValidDeviceTypeDomain()
        {
            return new DeviceTypeDomain()
            {
                DeviceTypeId = 1,
                Name = "Test Device Type",
                IsActive = true,
                Actions = new List<ActionDomain>(),
                Properties = new List<PropertyDomain>()
            };
        }
        #endregion
    }
}
