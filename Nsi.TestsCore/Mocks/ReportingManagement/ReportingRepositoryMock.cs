using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NSI.Domain.ReportingManagement;
using NSI.Repository.Interfaces.ReportingManagement;

namespace Nsi.TestsCore.Mocks.ReportingManagement
{
    public static class ReportingRepositoryMock
    {
        public static Mock<IReportingRepository> GetReportingRepositoryMock()
        {
            var reportingRepository = new Mock<IReportingRepository> { CallBase = false };

            reportingRepository.Setup(x => x.GetActiveUsers(1)).Returns(new UsersActivityWrapper
            {
                ActiveUsers = 2,
                InactiveUsers = 2,
                Users = new List<UserData>
                                {
                                    new UserData(),
                                    new UserData(),
                                    new UserData(),
                                    new UserData()
                                }
            });
            reportingRepository.Setup(x => x.GetFrequentDevices()).Returns(
                new List<FrequentDevicesWrapper> { new FrequentDevicesWrapper() });

            reportingRepository.Setup(x => x.GetFrequentIncidents(1, DateTime.Parse("2017/12/11 07:15:25 AM"), DateTime.Parse("2018/12/11 07:15:25 PM"))).Returns(
                new List<FrequentIncidentsWrapper> {
                    new FrequentIncidentsWrapper(),
                    new FrequentIncidentsWrapper()
                });

            reportingRepository.Setup(x => x.GetSmsSendingData(DateTime.Parse("2017/12/11 07:15:25 AM"), DateTime.Parse("2018/12/11 07:15:25 PM"))).Returns(
                new SmsDataWrapper
                {
                    SmsCount = 1,
                    Sms = new List<Sms> {new Sms(),
                    new Sms(),
                    new Sms()
                    }
                });

            reportingRepository.Setup(x => x.GetUserLoggingData(DateTime.Parse("2017/12/11 07:15:25 AM"), DateTime.Parse("2018/12/11 07:15:25 PM"))).Returns(
                new List<LoggingDataWrapper> {
                new LoggingDataWrapper(),
                new LoggingDataWrapper()});

            return reportingRepository;
        }
    }
}
