using Moq;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.Notifications;
using System;
using System.Collections.Generic;

namespace Nsi.TestsCore.Mocks.Notifications
{
    public static class NotificationRepositoryMock
    {
        public static Mock<INotificationRepository> GetNotificationRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var notificationRepository = new Mock<INotificationRepository> { CallBase = false };

            notificationRepository.Setup(x => x.Add(It.IsAny<NotificationDomain>())).Returns(1);

            notificationRepository.Setup(x => x.GetById(2)).Returns((NotificationDomain)null);

            notificationRepository.Setup(x => x.GetByCreatedDate(new DateTime().Date)).Returns(new List<NotificationDomain>() {
               new NotificationDomain {
                   Id = 1,
                   Content = "Test content",
                   NotificationStatusId = 2,
                   NotificationTypeId = 2,
                   ExternalId = new Guid(),
                   Title = "TEST",
               }
            });

            notificationRepository.Setup(x => x.GetByExternalId(new Guid())).Returns(new List<NotificationDomain>() {
               new NotificationDomain {
                   Id = 1,
                   Content = "Test content",
                   NotificationStatusId = 2,
                   NotificationTypeId = 2,
                   ExternalId = new Guid(),
                   Title = "TEST",
               }
            });

            notificationRepository.Setup(x => x.GetById(1)).Returns(
                new NotificationDomain
                {
                    Id = 1,
                    Content = "Test content",
                    NotificationStatusId = 2,
                    NotificationTypeId = 2,
                    ExternalId = new Guid(),
                    Title = "TEST",
                });

            return notificationRepository;

        }
    }
}
