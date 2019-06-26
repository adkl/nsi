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
    public class DeviceManipulationTests
    {
        private Mock<IDeviceRepository> _deviceRepositoryMock;
        private Mock<IIncidentRepository> _incidentRepositoryMock;
        private DeviceManipulation _deviceManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _deviceRepositoryMock = DeviceRepositoryMock.GetDeviceRepositoryMock();
            _incidentRepositoryMock = IncidentRepositoryMock.GetIncidentRepositoryMock();
            _deviceManipulation = new DeviceManipulation(
                _deviceRepositoryMock.Object,
                _incidentRepositoryMock.Object
                );
            _incidentRepositoryMock.Setup(x => x.GetAllIncidents(1)).Returns(GetListOfValidIncidentDomains());
        }

        #region Get Device By ID - Tests
        [TestMethod, TestCategory("Devices - Get Device By Id")]
        public void GetDeviceById_Success()
        {
            DeviceDomain device = _deviceManipulation.GetDeviceById(1);
            Assert.AreEqual(1, device.DeviceId);
        }

        [TestMethod, TestCategory("Devices - Get Device By Id")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Device with provided id does not exist.", SeverityEnum.Error)]
        public void GetDeviceById_Fail_InvalidDeviceId()
        {
            _deviceManipulation.GetDeviceById(-1);
        }
        #endregion

        #region Get All Devices - Tests
        [TestMethod, TestCategory("Devices - Get All Devices")]
        public void GetAllDevices_Success()
        {
            UserDomain user = GetValidUserDomain();
            _deviceManipulation.GetAllDevices(user.TenantId);
        }

        [TestMethod, TestCategory("Devices - Get All Devices")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetAllDevices_Fail_InvalidTenantId()
        {
            _deviceManipulation.GetAllDevices(-1);
        }
        #endregion

        #region Get All Active Devices - Tests
        [TestMethod, TestCategory("Devices - Get All Active Devices")]
        public void GetAllActiveDevices_Success()
        {
            UserDomain user = GetValidUserDomain();
            _deviceManipulation.GetAllActiveDevices(user.TenantId);
        }

        [TestMethod, TestCategory("Devices - Get All Active Devices")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetAllActiveDevices_Fail_InvalidTenantId()
        {
            _deviceManipulation.GetAllActiveDevices(-1);
        }
        #endregion

        #region Get All Inactive Devices - Tests
        [TestMethod, TestCategory("Devices - Get All Inactive Devices")]
        public void GetAllInactiveDevices_Success()
        {
            UserDomain user = GetValidUserDomain();
            _deviceManipulation.GetAllInactiveDevices(user.TenantId);
        }

        [TestMethod, TestCategory("Devices - Get All Inactive Devices")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetAllInactiveDevices_Fail_InvalidTenantId()
        {
            _deviceManipulation.GetAllInactiveDevices(-1);
        }
        #endregion

        #region Create Device - Tests
        [TestMethod, TestCategory("Devices - Create Device")]
        public void CreateDevice_Success()
        {
            int deviceId = _deviceManipulation.CreateDevice(GetValidCreateDeviceDomain(), GetValidUserDomain());
            Assert.AreEqual(1, deviceId);
        }

        [TestMethod, TestCategory("Devices - Create Device")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void CreateDevice_Fail_InvalidDevice()
        {
            _deviceManipulation.CreateDevice(null, new UserDomain());
        }

        [TestMethod, TestCategory("Devices - Create Device")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void CreateDevice_Fail_InvalidUser()
        {
            _deviceManipulation.CreateDevice(GetValidCreateDeviceDomain(), null);
        }
        #endregion

        #region Update Device - Tests
        [TestMethod, TestCategory("Devices - Update Device")]
        public void UpdateDevice_Success()
        {
            int deviceId = _deviceManipulation.UpdateDevice(GetValidUpdateDeviceDomain(), GetValidUserDomain());
            Assert.AreEqual(1, deviceId);
        }

        [TestMethod, TestCategory("Devices - Update Device")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void UpdateDevice_Fail_InvalidDevice()
        {
            _deviceManipulation.UpdateDevice(null, GetValidUserDomain());
        }

        [TestMethod, TestCategory("Devices - Update Device")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void UpdateDevice_Fail_InvalidUser()
        {
            _deviceManipulation.UpdateDevice(GetValidUpdateDeviceDomain(), null);
        }
        #endregion

        #region Delete Device - Tests
        [TestMethod, TestCategory("Devices - Delete Device")]
        public void DeleteDevice_Success()
        {
            bool isDeleted = _deviceManipulation.DeleteDevice(1, GetValidUserDomain());
            Assert.AreEqual(true, isDeleted);
        }

        [TestMethod, TestCategory("Devices - Delete Device")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Device with provided id does not exist.", SeverityEnum.Error)]
        public void DeleteDevice_Fail_InvalidDeviceId()
        {
            _deviceManipulation.DeleteDevice(-1, GetValidUserDomain());
        }
        #endregion

        #region Search Devices - Tests
        [TestMethod, TestCategory("Devices - Search Devices")]
        public void SearchDevices_Success()
        {
            UserDomain user = GetValidUserDomain();
            ICollection<DeviceDomain> devices = _deviceManipulation.SearchDevices(user.TenantId, "Device 2");

            Assert.AreEqual(1, devices.Count);
        }

        [TestMethod, TestCategory("Devices - Search Devices")]
        public void SearchDevicesWithFilter_Success()
        {
            UserDomain user = GetValidUserDomain();
            ICollection<DeviceDomain> devices = _deviceManipulation.SearchDevices(user.TenantId, "Device", true);

            Assert.AreEqual(1, devices.Count);
        }

        [TestMethod, TestCategory("Devices - Search Devices")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void SearchDevices_Fail_InvalidTenantId()
        {
            _deviceManipulation.SearchDevices(-2, "Device 2");
        }

        [TestMethod, TestCategory("Devices - Search Devices")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void SearchDevicesWithFilter_Fail_InvalidTenantId()
        {
            _deviceManipulation.SearchDevices(-2, "Device", true);
        }
        #endregion

        #region Get Number Of Incidents - Tests
        [TestMethod, TestCategory("Devices - Get Number Of Incidents")]
        public void GetNumberOfIncidents_Success()
        {
            int numberOfIncidents = _deviceManipulation.GetNumberOfIncidents(1, 7, 1);
            Assert.AreEqual(1, numberOfIncidents);
        }
        #endregion

        #region Valid Domain Models
        private CreateDeviceDomain GetValidCreateDeviceDomain()
        {
            return new CreateDeviceDomain()
            {
                Name = "Device Test",
                Description = "Test",
                DeviceTypeId = 1,
                DeviceImage = "FakeUrl"
            };
        }

        private UpdateDeviceDomain GetValidUpdateDeviceDomain()
        {
            return new UpdateDeviceDomain()
            {
                Name = "Device Test",
                Description = "Test",
                DeviceTypeId = 1,
                DeviceImage = "FakeUrl",
                IsActive = false,
                IsDeleted = true
            };
        }

        private UserDomain GetValidUserDomain()
        {
            return new UserDomain()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                TenantId = 1,
                IsActive = true
            };
        }

        private List<IncidentDomain> GetListOfValidIncidentDomains()
        {
            return new List<IncidentDomain>() {
                new IncidentDomain()
                {
                    Device = new DeviceDomain
                    {
                        DeviceId = 1
                    },
                    DateCreated = DateTime.Now
                },
                new IncidentDomain()
                {
                    Device = new DeviceDomain
                    {
                        DeviceId = 1
                    },
                    DateCreated = DateTime.Now.AddDays(-10)
                }
            };
        }
        #endregion
    }
}
