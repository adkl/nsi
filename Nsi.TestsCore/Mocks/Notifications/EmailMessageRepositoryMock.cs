using Moq;
using NSI.Common.Models;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.Notifications;
using System.Collections.Generic;


namespace Nsi.TestsCore.Mocks.Notifications
{
    public static class EmailMessageRepositoryMock
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
            ColumnName = "from",
            FilterTerm = "test",
            IsExactMatch = false
        };

        public static IList<FilterCriteria> filterCriteriaList = new List<FilterCriteria>
        {
            filterCriteria
        };

        private static SortCriteria sortCriteria = new SortCriteria
        {
            ColumnName = "from",
            Order = System.Data.SqlClient.SortOrder.Ascending,
            Priority = 1
        };

        public static IList<SortCriteria> sortCriteriaList = new List<SortCriteria>
        {
           sortCriteria
        };

        public static Mock<IEmailMessageRepository> GetEmailMessageRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var emailMessageRepository = new Mock<IEmailMessageRepository> { CallBase = false };

            emailMessageRepository.Setup(x => x.Add(It.IsAny<EmailMessageDomain>())).Returns(1);

            emailMessageRepository.Setup(x => x.GetById(1)).Returns(
                new EmailMessageDomain
                {
                    From = "test@gmail.com",
                    Id = 1,
                    NotificationId = 1
                });

            emailMessageRepository.Setup(x => x.GetByFrom("test@gmail.com")).Returns(
                new EmailMessageDomain
                {
                    From = "test@gmail.com",
                    Id = 1,
                    NotificationId = 1
                });

          
            emailMessageRepository.Setup(x => x.SearchEmailMessages(paging, filterCriteriaList, sortCriteriaList)).Returns(
                new List<EmailMessageDomain>
                {
                    new EmailMessageDomain
                    {
                        From = "test@gmail.com",
                        Id = 1,
                        NotificationId = 1
                    }
                });

            emailMessageRepository.Setup(x => x.GetAll()).Returns(
                new List<EmailMessageDomain>
                {
                    new EmailMessageDomain
                    {
                        From = "test@gmail.com",
                        Id = 1,
                        NotificationId = 1
                    }
                });

            return emailMessageRepository;
        }
    }
}
