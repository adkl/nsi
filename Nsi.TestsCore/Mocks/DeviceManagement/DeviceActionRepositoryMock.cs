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
    public static class DeviceActionRepositoryMock
    {
        public static Mock<IDeviceActionRepository> GetDeviceActionRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var deviceActionRepository = new Mock<IDeviceActionRepository> { CallBase = false };

            #region Get all Device Actions
            // Get all Device Actions
            deviceActionRepository.Setup(x => x.GetAllActions(1)).Returns(new List<ActionDomain>() {
                new ActionDomain()
                {
                    ActionId = 1,
                    IsActive = true,
                    Name = "Test Action 1"
                },
                new ActionDomain()
                {
                    ActionId = 2,
                    IsActive = true,
                    Name = "Test Action 2"
                }
            });

            #endregion

            return deviceActionRepository;
        }
    }
}
