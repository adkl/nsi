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
    public static class DeviceTypeRepositoryMock
    {
        public static Mock<IDeviceTypeRepository> GetDeviceTypeRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var deviceTypeRepository = new Mock<IDeviceTypeRepository> { CallBase = false };

            #region Get Device Type By ID
            //Get Device By ID
            deviceTypeRepository.Setup(x => x.GetDeviceTypeById(1)).Returns(
                new DeviceTypeDomain()
                {
                    DeviceTypeId = 1,
                    IsActive = true,
                    Name = "Test Device Type",
                    Actions = new List<ActionDomain>(),
                    Properties = new List<PropertyDomain>()
                });
            #endregion

            #region Get all Devices
            // Get all Device Types
            deviceTypeRepository.Setup(x => x.GetAllDeviceTypes()).Returns(new List<DeviceTypeDomain>() {
                new DeviceTypeDomain()
                {
                    DeviceTypeId = 1,
                    IsActive = true,
                    Name = "Test Device Type 1",
                    Actions = new List<ActionDomain>(),
                    Properties = new List<PropertyDomain>()
                },
                new DeviceTypeDomain()
                {
                    DeviceTypeId = 2,
                    IsActive = false,
                    Name = "Test Device Type 2",
                    Actions = new List<ActionDomain>(),
                    Properties = new List<PropertyDomain>()
                }
            });

            #endregion

            #region Create Device Type
            //Create Device Type
            deviceTypeRepository.Setup(x => x.CreateDeviceType(It.IsAny<DeviceTypeDomain>()))
                .Returns(1);
            #endregion

            #region Update Device Type
            //Update Device Type
            deviceTypeRepository.Setup(x => x.UpdateDeviceType(It.IsAny<DeviceTypeDomain>()))
                .Returns(1);
            #endregion

            #region Delete Device
            //Delete Device
            deviceTypeRepository.Setup(x => x.DeleteDeviceType(1))
                .Returns(true);
            #endregion

            #region Get all Active Device Types
            //Get all Active Device Types
            deviceTypeRepository.Setup(x => x.GetAllActiveDeviceTypes()).Returns(new List<DeviceTypeDomain>() {
                new DeviceTypeDomain()
                {
                    DeviceTypeId = 1,
                    IsActive = true,
                    Name = "Test Device Type 1",
                    Actions = new List<ActionDomain>(),
                    Properties = new List<PropertyDomain>()
                }
            });
            #endregion

            #region Get all Inactive Device Types
            // Get all Inactive Device Types
            deviceTypeRepository.Setup(x => x.GetAllInactiveDeviceTypes()).Returns(new List<DeviceTypeDomain>() {
                new DeviceTypeDomain()
                {
                    DeviceTypeId = 2,
                    IsActive = false,
                    Name = "Test Device Type 2",
                    Actions = new List<ActionDomain>(),
                    Properties = new List<PropertyDomain>()
                }
            });
            #endregion

            #region Search Device Types without filter
            // Search Device Types without filter
            deviceTypeRepository.Setup(x => x.SearchDeviceTypes("Device Type 2")).Returns(new List<DeviceTypeDomain>() {
                new DeviceTypeDomain()
                {
                    DeviceTypeId = 2,
                    IsActive = false,
                    Name = "Test Device Type 2",
                    Actions = new List<ActionDomain>(),
                    Properties = new List<PropertyDomain>()
                }
            });
            #endregion

            #region Search Device Types with Active filter
            // Search Device Types with Active filter
            deviceTypeRepository.Setup(x => x.SearchDeviceTypes("Device Type 2", true)).Returns(new List<DeviceTypeDomain>() {
                new DeviceTypeDomain()
                {
                    DeviceTypeId = 1,
                    IsActive = true,
                    Name = "Test Device Type 1",
                    Actions = new List<ActionDomain>(),
                    Properties = new List<PropertyDomain>()
                }
            });
            #endregion

            return deviceTypeRepository;
        }
    }
}
