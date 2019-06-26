using Moq;
using NSI.BusinessLogic.Interfaces.DeviceManagement;
using NSI.Domain.DeviceManagement;
using NSI.Domain.Membership;
using System.Collections.Generic;

namespace Nsi.TestsCore.Mocks.DeviceManagement
{
    public static class DeviceTypeManipulationMock
    {
        public static Mock<IDeviceTypeManipulation> GetDeviceTypeManipulationMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var deviceTypeManipulation = new Mock<IDeviceTypeManipulation> { CallBase = false };

            #region Get Device By ID
            //Get Device By ID
            deviceTypeManipulation.Setup(x => x.GetDeviceTypeById(1)).Returns(
                new DeviceTypeDomain()
                {
                    DeviceTypeId = 1,
                    Name = "Test Device",
                    Code = "100",
                    Actions = new List<ActionDomain>(),
                    Properties = new List<PropertyDomain>(),
                    IsActive = true
                });

            deviceTypeManipulation.Setup(x => x.GetDeviceTypeById(55)).Returns((DeviceTypeDomain)null);

            #endregion

            #region Get all Device Types
            // Get all Devices
            deviceTypeManipulation.Setup(x => x.GetAllDeviceTypes()).Returns(new List<DeviceTypeDomain>() {
               new DeviceTypeDomain()
                {
                    DeviceTypeId = 1,
                    Name = "Test Device Type 1",
                    Code = "100",
                    Actions = new List<ActionDomain>(),
                    Properties = new List<PropertyDomain>(),
                    IsActive = true
                },
                new DeviceTypeDomain()
                {
                    DeviceTypeId = 2,
                    Name = "Test Device Type 2",
                    Code = "200",
                    Actions = new List<ActionDomain>(),
                    Properties = new List<PropertyDomain>(),
                    IsActive = true
                }
            });

            #endregion

            #region Create Device
            //Create Device
            deviceTypeManipulation.Setup(x => x.CreateDeviceType(It.IsAny<DeviceTypeDomain>()))
                .Returns(1);

            deviceTypeManipulation.Setup(x => x.CreateDeviceType(null))
                .Returns(0);
            #endregion

            #region Update Device
            //Update Device
            deviceTypeManipulation.Setup(x => x.UpdateDeviceType(It.IsAny<DeviceTypeDomain>()))
                .Returns(1);

            deviceTypeManipulation.Setup(x => x.UpdateDeviceType(null))
                .Returns(0);
            #endregion

            #region Delete Device
            //Delete Device
            deviceTypeManipulation.Setup(x => x.DeleteDeviceType(1))
                .Returns(true);

            deviceTypeManipulation.Setup(x => x.DeleteDeviceType(55))
                .Returns(false);
            #endregion

            #region Get all Active Devices
            //Get all Active Devices
            deviceTypeManipulation.Setup(x => x.GetAllActiveDeviceTypes()).Returns(new List<DeviceTypeDomain>() {
                new DeviceTypeDomain()
                {
                    DeviceTypeId = 1,
                    Name = "Test Device Type 1",
                    Code = "100",
                    Actions = new List<ActionDomain>(),
                    Properties = new List<PropertyDomain>(),
                    IsActive = true
                },
                new DeviceTypeDomain()
                {
                    DeviceTypeId = 2,
                    Name = "Test Device Type 2",
                    Code = "200",
                    Actions = new List<ActionDomain>(),
                    Properties = new List<PropertyDomain>(),
                    IsActive = true
                }
            });
            #endregion

            #region Get all Inactive Devices
            // Get all Inactive Devices
            deviceTypeManipulation.Setup(x => x.GetAllInactiveDeviceTypes()).Returns(new List<DeviceTypeDomain>() {
                new DeviceTypeDomain()
                {
                    DeviceTypeId = 2,
                    Name = "Test Device Type 3",
                    Code = "300",
                    Actions = new List<ActionDomain>(),
                    Properties = new List<PropertyDomain>(),
                    IsActive = true
                }
            });
            #endregion

            #region Search Devices without filter
            // Search Devices without filter
            deviceTypeManipulation.Setup(x => x.SearchDeviceTypes("Device Type 2")).Returns(new List<DeviceTypeDomain>() {
                new DeviceTypeDomain()
                {
                    DeviceTypeId = 2,
                    Name = "Test Device Type 2",
                    Code = "200",
                    Actions = new List<ActionDomain>(),
                    Properties = new List<PropertyDomain>(),
                    IsActive = true
                }
            });
            #endregion

            #region Search Devices with Active filter
            // Search Devices with Active filter
            deviceTypeManipulation.Setup(x => x.SearchDeviceTypes("Device Type", true)).Returns(new List<DeviceTypeDomain>() {
               new DeviceTypeDomain()
                {
                    DeviceTypeId = 1,
                    Name = "Test Device Type 1",
                    Code = "100",
                    Actions = new List<ActionDomain>(),
                    Properties = new List<PropertyDomain>(),
                    IsActive = true
                },
                new DeviceTypeDomain()
                {
                    DeviceTypeId = 2,
                    Name = "Test Device Type 2",
                    Code = "200",
                    Actions = new List<ActionDomain>(),
                    Properties = new List<PropertyDomain>(),
                    IsActive = true
                }
            });
            #endregion

            return deviceTypeManipulation;
        }
    }
}
