using Moq;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks.Notifications
{
    public static class WebNotificationRepositoryMock
    {
        public static Mock<IWebNotificationRepository> GetWebNotificationRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var webNotificationRepository = new Mock<IWebNotificationRepository> { CallBase = false };

            webNotificationRepository.Setup(x => x.GetById(1)).Returns(new WebNotificationDomain
            {
                Id = 1
            });

            webNotificationRepository.Setup(x => x.Add(It.IsAny<WebNotificationDomain>())).Returns(1);

            return webNotificationRepository;
        }
    }
}
