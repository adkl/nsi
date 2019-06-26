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
    public static class NotificationUserMock
    {
        public static Mock<INotificationUserRepository> GetNotificationUserRepositoryMock()
        {
            var notificationUserRepository = new Mock<INotificationUserRepository> { CallBase = false };


            notificationUserRepository.Setup(x => x.Add(It.IsAny<NotificationUserDomain>()))
            .Returns(1);

            // returns null for TESTDOESNOTEXIST code
            notificationUserRepository.Setup(x => x.GetById(2)).Returns((NotificationUserDomain)null);

            notificationUserRepository.Setup(x => x.GetById(1)).Returns(
            new NotificationUserDomain
            {
                Id = 1,
                UserTenantId = 1,
                NotificationId = 1,
                UserInfoId = 1
            });

            notificationUserRepository.Setup(x => x.GetByNotificationId(1)).Returns(
                new List<NotificationUserDomain> {
                    new NotificationUserDomain
                    {
                        Id = 1,
                        NotificationId = 1,
                        UserTenantId = 1,
                        UserInfoId = 1
                    }
                });

            notificationUserRepository.Setup(x => x.GetByUserId(1)).Returns(
                new List<NotificationUserDomain> {
                    new NotificationUserDomain
                    {
                        Id = 1,
                        NotificationId = 1,
                        UserTenantId = 1,
                        UserInfoId = 1
                    }
                });

            notificationUserRepository.Setup(x => x.GetAll()).Returns(
            new List<NotificationUserDomain> {
                    new NotificationUserDomain
                    {
                        Id = 1,
                        NotificationId = 1,
                        UserTenantId = 1,
                        UserInfoId = 1
                    }
            });

            return notificationUserRepository;
        }
    }
}
