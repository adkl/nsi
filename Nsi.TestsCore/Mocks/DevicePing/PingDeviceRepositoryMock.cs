using Moq;
using NSI.Domain.DeviceManagement;
using NSI.Repository.Interfaces.DeviceManagement;

namespace Nsi.TestsCore.Mocks.DevicePing
{
    public static class PingDeviceRepositoryMock
    {
        public static Mock<IDeviceRepository> GetDeviceRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var deviceRepository = new Mock<IDeviceRepository> { CallBase = false };

            #region Get Device By ID
            //Get active device
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

            //Get inactive device
            deviceRepository.Setup(x => x.GetDeviceById(2)).Returns(
                new DeviceDomain()
                {
                    DeviceId = 2,
                    IsActive = false,
                    TenantId = 1,
                    Name = "Test Device",
                    Description = "Test Device Description",
                    DeviceTypeId = 1
                });
            #endregion

            return deviceRepository;
        }
    }
}
