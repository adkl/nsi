using Moq;
using NSI.Domain.DeviceManagement;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks.DeviceManagement
{
    public static class DeviceRepositoryMock
    {
        public static Mock<IDeviceRepository> GetDeviceRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var deviceRepository = new Mock<IDeviceRepository> { CallBase = false };

            #region Get Device By ID
            //Get Device By ID
            deviceRepository.Setup(x => x.GetDeviceById(1)).Returns(
                new DeviceDomain()
                {
                    DeviceId = 1,
                    IsActive = true,
                    TenantId = 1,
                    Name = "Test Device",
                    Description = "Test Device Description",
                    DeviceTypeId = 1
                });
            #endregion

            #region Get all Devices
            // Get all Devices
            deviceRepository.Setup(x => x.GetAllDevices(1)).Returns(new List<DeviceDomain>() {
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
            deviceRepository.Setup(x => x.CreateDevice(It.IsAny<CreateDeviceDomain>(), It.IsAny<UserDomain>()))
                .Returns(1);
            #endregion

            #region Update Device
            //Update Device
            deviceRepository.Setup(x => x.UpdateDevice(It.IsAny<UpdateDeviceDomain>(), It.IsAny<UserDomain>()))
                .Returns(1);
            #endregion

            #region Delete Device
            //Delete Device
            deviceRepository.Setup(x => x.DeleteDevice(1, It.IsAny<UserDomain>()))
                .Returns(true);
            #endregion

            #region Get all Active Devices
            //Get all Active Devices
            deviceRepository.Setup(x => x.GetAllActiveDevices(1)).Returns(new List<DeviceDomain>() {
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
            deviceRepository.Setup(x => x.GetAllInactiveDevices(1)).Returns(new List<DeviceDomain>() {
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
            deviceRepository.Setup(x => x.SearchDevices(1, "Device 2")).Returns(new List<DeviceDomain>() {
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
            deviceRepository.Setup(x => x.SearchDevices(1, "Device", true)).Returns(new List<DeviceDomain>() {
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

            return deviceRepository;
        }
    }
}
