using Moq;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.Notifications;
using System.Collections.Generic;

namespace Nsi.TestsCore.Mocks.Notifications
{
    public static class EmailRecipientTypeRepositoryMock
    {
        public static Mock<IEmailRecipientTypeRepository> GetEmailRecipientTypeRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var emailRecipientTypeRepository = new Mock<IEmailRecipientTypeRepository> { CallBase = false };

            emailRecipientTypeRepository.Setup(x => x.Add(It.IsAny<EmailRecipientTypeDomain>())).Returns(1);

            emailRecipientTypeRepository.Setup(x => x.GetById(1)).Returns(
                new EmailRecipientTypeDomain
                {
                    Name = "Name",
                    Id = 1,
                    Code = "code1"
                });

            emailRecipientTypeRepository.Setup(x => x.GetByCode("code1")).Returns(
                new EmailRecipientTypeDomain
                {
                    Name = "Name",
                    Id = 1,
                    Code = "code1"
                });

            emailRecipientTypeRepository.Setup(x => x.GetAll()).Returns(
                new List<EmailRecipientTypeDomain>
                {
                    new EmailRecipientTypeDomain
                    {
                        Name = "Name",
                        Id = 1,
                        Code = "code1"
                    }
                });

            return emailRecipientTypeRepository;
        }

    }
}
