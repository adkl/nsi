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
    public static class DevicePropertyRepositoryMock
    {
        public static Mock<IDevicePropertyRepository> GetDevicePropertyRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var devicePropertyRepository = new Mock<IDevicePropertyRepository> { CallBase = false };

            #region Get all Device Properties
            // Get all Device Properties
            devicePropertyRepository.Setup(x => x.GetAllProperties(1)).Returns(new List<PropertyDomain>() {
                new PropertyDomain()
                {
                    PropertyId = 1,
                    IsActive = true,
                    Name = "Test Property 1"
                },
                new PropertyDomain()
                {
                    PropertyId = 2,
                    IsActive = true,
                    Name = "Test Property 2"
                }
            });

            #endregion

            return devicePropertyRepository;
        }
    }
}
