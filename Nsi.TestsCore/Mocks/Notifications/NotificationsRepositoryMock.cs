using Moq;
using NSI.Repository.Interfaces.IncidentManagement;
using NSI.Repository.Interfaces.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks
{
    public static class NotificationRepositoryMock
    {
        public static Mock<INotificationRepository> GetNotificationRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var NotificationRepository = new Mock<INotificationRepository> { CallBase = false };
            
            NotificationRepository.Setup(x => x.Add(It.IsAny<NSI.Domain.Notifications.NotificationDomain>())).Returns(1);

            return NotificationRepository;
        }
    }
}
