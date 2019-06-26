using Moq;
using NSI.BusinessLogic.Interfaces.DeviceManagement;
using NSI.Domain.DeviceManagement;
using NSI.Domain.Membership;
using System.Collections.Generic;

namespace Nsi.TestsCore.Mocks.DeviceManagement
{
    public static class DeviceManipulationMock
    {
        public static Mock<IDeviceManipulation> GetDeviceManipulationMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var deviceManipulation = new Mock<IDeviceManipulation> { CallBase = false };

            #region Get Device By ID
            //Get Device By ID
            deviceManipulation.Setup(x => x.GetDeviceById(1)).Returns(
                new DeviceDomain()
                {
                    DeviceId = 1,
                    IsActive = true,
                    TenantId = 1,
                    Name = "Test Device",
                    Description = "Test Device Description",
                    DeviceTypeId = 1
                });

            deviceManipulation.Setup(x => x.GetDeviceById(55)).Returns((DeviceDomain)null);

            #endregion

            #region Get all Devices
            // Get all Devices
            deviceManipulation.Setup(x => x.GetAllDevices(1)).Returns(new List<DeviceDomain>() {
                new DeviceDomain()
                {
                    Id = 1,
                    IsActive = true,
                    TenantId = 1,
                    Name = "Test Device 1",
                    Description = "Test Device Description",
                    DeviceTypeId = 1,
                    IsDeleted = false,
                    DeviceImage = "fakeUrl"
                },
                new DeviceDomain()
                {
                    Id = 2,
                    IsActive = false,
                    TenantId = 1,
                    Name = "Test Device 2",
                    Description = "Test Device Description",
                    DeviceTypeId = 2,
                    IsDeleted = false,
                    DeviceImage = "fakeUrl"
                }
            });

            #endregion

            #region Create Device
            //Create Device
            deviceManipulation.Setup(x => x.CreateDevice(It.IsAny<CreateDeviceDomain>(), It.IsAny<UserDomain>()))
                .Returns(1);

            deviceManipulation.Setup(x => x.CreateDevice(It.IsAny<CreateDeviceDomain>(), null))
                .Returns(0);
            #endregion

            #region Update Device
            //Update Device
            deviceManipulation.Setup(x => x.UpdateDevice(It.IsAny<UpdateDeviceDomain>(), It.IsAny<UserDomain>()))
                .Returns(1);

            deviceManipulation.Setup(x => x.UpdateDevice(It.IsAny<UpdateDeviceDomain>(), null))
                .Returns(0);
            #endregion

            #region Delete Device
            //Delete Device
            deviceManipulation.Setup(x => x.DeleteDevice(1, It.IsAny<UserDomain>()))
                .Returns(true);

            deviceManipulation.Setup(x => x.DeleteDevice(55, It.IsAny<UserDomain>()))
                .Returns(false);
            #endregion

            #region Get all Active Devices
            //Get all Active Devices
            deviceManipulation.Setup(x => x.GetAllActiveDevices(1)).Returns(new List<DeviceDomain>() {
                new DeviceDomain()
                {
                    Id = 1,
                    IsActive = true,
                    TenantId = 1,
                    Name = "Test Device 1",
                    Description = "Test Device Description",
                    DeviceTypeId = 1,
                    IsDeleted = false,
                    DeviceImage = "fakeUrl"
                }
            });
            #endregion

            #region Get all Inactive Devices
            // Get all Inactive Devices
            deviceManipulation.Setup(x => x.GetAllInactiveDevices(1)).Returns(new List<DeviceDomain>() {
                new DeviceDomain()
                {
                    Id = 2,
                    IsActive = false,
                    TenantId = 1,
                    Name = "Test Device 2",
                    Description = "Test Device Description",
                    DeviceTypeId = 2,
                    IsDeleted = false,
                    DeviceImage = "fakeUrl"
                }
            });
            #endregion

            #region Search Devices without filter
            // Search Devices without filter
            deviceManipulation.Setup(x => x.SearchDevices(1, "Device 2")).Returns(new List<DeviceDomain>() {
                new DeviceDomain()
                {
                    Id = 2,
                    IsActive = false,
                    TenantId = 1,
                    Name = "Test Device 2",
                    Description = "Test Device Description",
                    DeviceTypeId = 2,
                    IsDeleted = false,
                    DeviceImage = "fakeUrl"
                }
            });
            #endregion

            #region Search Devices with Active filter
            // Search Devices with Active filter
            deviceManipulation.Setup(x => x.SearchDevices(1, "Device", true)).Returns(new List<DeviceDomain>() {
                new DeviceDomain()
                {
                    Id = 2,
                    IsActive = true,
                    TenantId = 1,
                    Name = "Test Device 1",
                    Description = "Test Device Description",
                    DeviceTypeId = 1,
                    IsDeleted = false,
                    DeviceImage = "fakeUrl"
                }
            });
            #endregion

            #region Get Number Of Incidents
            // Get Number Of Incidents
            deviceManipulation.Setup(x => x.GetNumberOfIncidents(1, 7, 1)).Returns(10);
            #endregion

            return deviceManipulation;
        }
    }
}
