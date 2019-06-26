using Moq;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.Notifications;
using System.Collections.Generic;

namespace Nsi.TestsCore.Mocks.Notifications {
    public static class NotificationTypeRepositoryMock {
        public static Mock<INotificationTypeRepository> GetNotificationTypeRepositoryMock() {
            // Always set CallBase to false, we don't want to really hit the DB
            var notificationTypeRepository = new Mock<INotificationTypeRepository> { CallBase = false };

            notificationTypeRepository.Setup(x => x.Add(It.IsAny<NotificationTypeDomain>()))
            .Returns(1);

            // returns null for TESTDOESNOTEXIST code
            notificationTypeRepository.Setup(x => x.GetByCode("TESTDOESNOTEXIST")).Returns((NotificationTypeDomain)null);

            notificationTypeRepository.Setup(x => x.GetByCode("TESTCODEEXISTS")).Returns(
                new NotificationTypeDomain {
                    Code = "TESTCODEEXISTS",
                    Name = "Test Notification Type",
                    Id = 1
                });

            notificationTypeRepository.Setup(x => x.GetById(2)).Returns(
            new NotificationTypeDomain {
                Code = "TESTDOESNOTEXIST",
                Name = "Test Notification Type",
                Id = 2
            });

            notificationTypeRepository.Setup(x => x.GetByName("TESTNAMEEXISTS")).Returns(
                    new NotificationTypeDomain
                    {
                        Code = "Test Notification Code",
                        Name = "TESTNAMEEXISTS",
                        Id = 3
                    }
                );

            notificationTypeRepository.Setup(x => x.GetById(4)).Returns((NotificationTypeDomain)null);

            notificationTypeRepository.Setup(x => x.GetAllByCode("TestCodeNotUnique")).Returns(new List<NotificationTypeDomain>
            {
                new NotificationTypeDomain()
            });

            notificationTypeRepository.Setup(x => x.GetAllByCode("TestCodeUnique")).Returns(new List<NotificationTypeDomain>());

            return notificationTypeRepository;
        }
    }
}
