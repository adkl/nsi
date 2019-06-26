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
    public static class NotificationStatusRepositoryMock
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
            ColumnName = "code",
            FilterTerm = "TESTDOESNOTEXIST",
            IsExactMatch = true
        };

        public static IList<FilterCriteria> filterCriteriaList = new List<FilterCriteria>
        {
            filterCriteria
        };

        private static SortCriteria sortCriteria = new SortCriteria
        {
            ColumnName = "code",
            Order = System.Data.SqlClient.SortOrder.Ascending,
            Priority = 1
        };

        public static IList<SortCriteria> sortCriteriaList = new List<SortCriteria>
        {
           sortCriteria
        };

        public static Mock<INotificationStatusRepository> GetNotificationStatusRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var notificationStatusRepository = new Mock<INotificationStatusRepository> { CallBase = false };


            notificationStatusRepository.Setup(x => x.AddNotificationStatus(It.IsAny<NotificationStatusDomain>()))
            .Returns(1);

            // returns null for TESTDOESNOTEXIST code
            notificationStatusRepository.Setup(x => x.GetByCode("TESTDOESNOTEXIST")).Returns((NotificationStatusDomain)null);

            notificationStatusRepository.Setup(x => x.GetByCode("TESTCODEEXISTS")).Returns(
                new NotificationStatusDomain
                {
                    Code = "TESTCODEEXISTS",
                    Name = "Test Notification Status",
                    Id = 1
                });

            notificationStatusRepository.Setup(x => x.GetById(2)).Returns(
            new NotificationStatusDomain
            {
                Code = "TESTDOESNOTEXIST",
                Name = "Test Notification Status",
                Id = 2
            });
            
            notificationStatusRepository.Setup(x => x.SearchNotificationStatus(paging, filterCriteriaList, sortCriteriaList)).Returns(
                    new List<NotificationStatusDomain>
                    {
                        new NotificationStatusDomain
                        {
                            Code = "TESTDOESNOTEXIST",
                            Name = "Test Notification Status",
                            Id = 1
                        }
                    }
                );
                
            return notificationStatusRepository;

           }
    }
}
