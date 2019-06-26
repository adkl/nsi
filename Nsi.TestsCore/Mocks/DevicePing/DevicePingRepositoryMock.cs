using Moq;
using NSI.Common.Models;
using NSI.Domain.DevicePing;
using NSI.Repository.Interfaces.DevicePing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks.DevicePing
{
    public static class DevicePingRepositoryMock
    {
        public static Mock<IDevicePingRepository> GetDevicePingRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var devicePingRepository = new Mock<IDevicePingRepository> { CallBase = false };

            #region Get Device Ping By ID
            //Get Device By ID
            devicePingRepository.Setup(x => x.GetDevicePingById(1, 1)).Returns(
                new DevicePingDomain()
                {
                    DeviceId = 1,
                    ActionId = 1,
                    TenantId = 1,
                    Id = 1,
                    RuleId = 1
                });
            #endregion

            #region Search Device Pings 
            // Search Device Pings Without filter

            devicePingRepository.Setup(x => x.Search(1, It.IsAny<Paging>(), It.IsAny<IList<FilterCriteria>>(), It.IsAny<IList<SortCriteria>>()))
                .Returns(new List<DevicePingDomain>() {
                    new DevicePingDomain()
                    {
                        DeviceId = 1,
                        ActionId = 1,
                        TenantId = 1,
                        Id = 1,
                        RuleId = 1
                    },
                    new DevicePingDomain()
                    {
                        DeviceId = 2,
                        ActionId = 4,
                        TenantId = 1,
                        Id = 2,
                        RuleId = 2
                    },
                    new DevicePingDomain()
                    {
                        DeviceId = 3,
                        ActionId = 2,
                        TenantId = 1,
                        Id = 3,
                        RuleId = 3
                    }
                });

            #endregion

            #region Create Device Ping
            //Create Device Ping
            devicePingRepository.Setup(x => x.AddDevicePing(1, It.IsAny<DevicePingDomain>()))
                .Returns(1);
            #endregion

            #region Delete Device Ping
            //Delete Device Ping
            devicePingRepository.Setup(x => x.DeleteDevicePingById(1, 1))
                .Returns(true);
            #endregion

            #region Device Last Ping
            devicePingRepository.Setup(x => x.LastDevicePingForDevice(1))
                .Returns(new DevicePingDomain()
                {
                    DeviceId = 1,
                    ActionId = 1,
                    TenantId = 1,
                    Id = 1,
                    RuleId = 1
                });
            #endregion

            #region  GetAllDeviceProperties
            devicePingRepository.Setup(x => x.GetAllDeviceProperties())
                .Returns(new List<DevicePropertyValue>
                {
                    new DevicePropertyValue
                    {
                        Id = 1,
                        DeviceId = 1,
                        DevicePingId = 1,
                        PropertyId = 1,
                        TenantId = 1,
                        Value = "TestValue"
                    }
                });
            #endregion

            return devicePingRepository;
        }
    }
}
