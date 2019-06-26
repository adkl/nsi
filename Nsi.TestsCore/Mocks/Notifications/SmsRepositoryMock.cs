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
    public static class SmsRepositoryMock
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
            ColumnName = "phonenumber",
            FilterTerm = "+38760111222",
            IsExactMatch = true
        };

        public static IList<FilterCriteria> filterCriteriaList = new List<FilterCriteria>
        {
            filterCriteria
        };

        private static SortCriteria sortCriteria = new SortCriteria
        {
            ColumnName = "phonenumber",
            Order = System.Data.SqlClient.SortOrder.Ascending,
            Priority = 1
        };

        public static IList<SortCriteria> sortCriteriaList = new List<SortCriteria>
        {
           sortCriteria
        };

        public static Mock<ISmsRepository> GetSmsRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var smsRepository = new Mock<ISmsRepository> { CallBase = false };

            smsRepository.Setup(x => x.Add(It.IsAny<SmsDomain>())).Returns(1);


            // returns null for TESTDOESNOTEXIST code
            smsRepository.Setup(x => x.GetByFrom("+1222333444")).Returns((SmsDomain)null);

            smsRepository.Setup(x => x.GetById(1)).Returns(
                new SmsDomain
                {
                    From = "+1555666777",
                    PhoneNumber = "+38760111222",
                    Id = 1,
                    NotificationId = 1
                });
            smsRepository.Setup(x => x.GetByFrom("+1555666777")).Returns(
                new SmsDomain
                {
                    From = "+1555666777",
                    PhoneNumber = "+38760111222",
                    Id = 1,
                    NotificationId = 1
                });

            smsRepository.Setup(x => x.GetByPhoneNumber("+38760111222")).Returns(
                new SmsDomain
                {
                    From = "+1222333444",
                    PhoneNumber = "+38760111222",
                    Id = 1,
                    NotificationId = 1
                });

            smsRepository.Setup(x => x.SearchSms(paging, filterCriteriaList, sortCriteriaList)).Returns(
                new List<SmsDomain>
                {
                    new SmsDomain
                    {
                        From = "+1222333444",
                        PhoneNumber = "+38760111222",
                        Id = 1,
                        NotificationId = 1
                    }
                });
            return smsRepository;

        }
    }
}
