using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSI.Common.Exceptions;
using NSI.Repository.Interfaces.ReportingManagement;
using NSI.BusinessLogic.ReportingManagement;
using NSI.Repository.ReportingManagement;
using Nsi.TestsCore.Mocks.ReportingManagement;
using Nsi.TestsCore.Extensions;
using NSI.Common.Enumerations;

namespace NSI.Tests.Business.ReportingManagement
{
    [TestClass]
    public class ReportingManipulationTests
    {
        private Mock<IReportingRepository> _ReportingManagementMock;
        private ReportingManipulation _ReportingManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _ReportingManagementMock = ReportingRepositoryMock.GetReportingRepositoryMock();
            _ReportingManipulation = new ReportingManipulation(_ReportingManagementMock.Object);
        }

        [TestMethod, TestCategory("Reporting - GetActiveUsers")]
        public void GetActiveUsers_Pass()
        {
            _ReportingManipulation.GetActiveUsers(1);
        }

        [TestMethod,TestCategory("Reporting - GetFrequentDevices")]
        public void GetFrequentDevices_Pass ()
        {
            _ReportingManipulation.GetFrequentDevices();
        }

        [TestMethod, TestCategory("Reporting - GetFrequentIncidents")]
        public void GetFrequentIncidents_Pass()
        {
            _ReportingManipulation.GetFrequentIncidents(1, DateTime.Parse("2017 / 12 / 11 07:15:25 AM"), DateTime.Parse("2018/12/11 07:15:25 PM"));
        }

        [TestMethod, TestCategory("Reporting - GetSMSSendingData")]
        public void GetSMSSendingData_Pass()
        {
            _ReportingManipulation.GetSmsSendingData(DateTime.Parse("2017/12/11 07:15:25 PM"), DateTime.Parse("2018/12/11 07:15:25 PM"));
        }

        [TestMethod, TestCategory("Reporting - GetSMSSendingData")]
        public void GetLoggingData_Pass()
        {
            _ReportingManipulation.GetUserLoggingData(DateTime.Parse("2017/12/11 07:15:25 PM"), DateTime.Parse("2018/12/11 07:15:25 PM"));
        }

    }
}
