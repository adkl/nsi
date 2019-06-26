using Moq;
using NSI.Common.Models;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks.Notifications
{
    public static class EmailRecipientRepositoryMock
    {
        public static Paging paging = new Paging
        {
            Pages = 1,
            Page = 1,
            RecordsPerPage = 10,
            TotalRecords = 10
        };

        private static FilterCriteria filterCriteria = new FilterCriteria
        {
            ColumnName = "emailaddress",
            FilterTerm = "com",
            IsExactMatch = false
        };

        public static IList<FilterCriteria> filterCriteriaList = new List<FilterCriteria>
        {
            filterCriteria
        };

        private static SortCriteria sortCriteria = new SortCriteria
        {
            ColumnName = "emailaddress",
            Order = System.Data.SqlClient.SortOrder.Ascending,
            Priority = 1
        };

        public static IList<SortCriteria> sortCriteriaList = new List<SortCriteria>
        {
           sortCriteria
        };

        public static Mock<IEmailRecipientRepository> GetEmailRecipientRepositoryMock()
        {
            var emailRecipientRepository = new Mock<IEmailRecipientRepository> { CallBase = false };

            emailRecipientRepository.Setup(x => x.Add(It.IsAny<EmailRecipientDomain>())).Returns(1);

            // returns null for TESTDOESNOTEXIST code
            emailRecipientRepository.Setup(x => x.GetByEmailAddress("nonexistent@email.com")).Returns((EmailRecipientDomain)null);

            emailRecipientRepository.Setup(x => x.GetById(1)).Returns(
                new EmailRecipientDomain
                {
                    EmailAddress = "test@email.com",
                    EmaiMessagelId = 1,
                    EmailRecipientTypeId = 1,                
                    Id = 1
                }
                );

            emailRecipientRepository.Setup(x => x.GetByEmailAddress("test@email.com")).Returns(
            new EmailRecipientDomain
            {
                EmailAddress = "test@email.com",
                EmaiMessagelId = 1,
                EmailRecipientTypeId = 1,
                Id = 1
            }
            );

            emailRecipientRepository.Setup(x => x.GetAll()).Returns(
                new List<EmailRecipientDomain>
                {
                    new EmailRecipientDomain
                    {
                        EmailAddress = "test@email.com",
                        EmaiMessagelId = 1,
                        EmailRecipientTypeId = 1,
                        Id = 1
                    },
                    new EmailRecipientDomain
                    {
                        EmailAddress = "test2@email.com",
                        EmaiMessagelId = 1,
                        EmailRecipientTypeId = 1,
                        Id = 2
                    },

                }
            );


            emailRecipientRepository.Setup(x => x.SearchEmailRecipients(paging, filterCriteriaList, sortCriteriaList)).Returns(
                new List<EmailRecipientDomain>
                {
                    new EmailRecipientDomain
                    {
                        EmailAddress = "test@email.com",
                        EmaiMessagelId = 1,
                        EmailRecipientTypeId = 1,
                        Id = 1
                    },
                    new EmailRecipientDomain
                    {
                        EmailAddress = "test2@email.com",
                        EmaiMessagelId = 1,
                        EmailRecipientTypeId = 1,
                        Id = 2
                    },
                }
            );

            return emailRecipientRepository;

        }
    }
}
