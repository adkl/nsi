using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nsi.TestsCore.Mocks;
using Nsi.TestsCore.Extensions;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.Notifications;
using Nsi.TestsCore.Mocks.Notifications;
using NSI.BusinessLogic.Notifications;
using NSI.Common.Exceptions;
using NSI.Common.Enumerations;
using NSI.Common.Models;

namespace NSI.Tests.Business.Notifications
{
    [TestClass]
    public class NotificationStatusTests
    {
        private Mock<INotificationStatusRepository> _notificationStatusRepositoryMock;
        private NotificationStatusManipulation _notificationStatusManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _notificationStatusRepositoryMock = NotificationStatusRepositoryMock.GetNotificationStatusRepositoryMock();
            _notificationStatusManipulation = new NotificationStatusManipulation(_notificationStatusRepositoryMock.Object);
        }

        [TestMethod, TestCategory("NotificationStatus - AddNotificationStatus")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification status not provided.", SeverityEnum.Warning)]
        public void AddNotificationStatus_Fail_ParameterNull()
        {
            _notificationStatusManipulation.AddNotificationStatus(null);
        }

        [TestMethod, TestCategory("NotificationStatus - AddNotificationStatus")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification status code not provided.", SeverityEnum.Warning)]
        public void AddNotificationStatus_Fail_CodeNull()
        {
            var NotificationStatus = GetValidNotificationStatus();
            NotificationStatus.Code = null;
            _notificationStatusManipulation.AddNotificationStatus(NotificationStatus);
        }

        [TestMethod, TestCategory("NotificationStatus - AddNotificationStatus")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification status name not provided.", SeverityEnum.Warning)]
        public void AddNotificationStatus_Fail_NameNull()
        {
            var NotificationStatus = GetValidNotificationStatus();
            NotificationStatus.Name = null;
            _notificationStatusManipulation.AddNotificationStatus(NotificationStatus);
        }

        [TestMethod, TestCategory("NotificationStatus - AddNotificationStatus")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Notification status with provided code already exists.", SeverityEnum.Warning)]
        public void AddNotificationStatus_Fail_CodeExists()
        {
            var NotificationStatus = GetValidNotificationStatus();
            NotificationStatus.Code = "TESTCODEEXISTS";
            _notificationStatusManipulation.AddNotificationStatus(NotificationStatus);
        }

        [TestMethod, TestCategory("NotificationStatus - AddNotificationStatus")]
        public void AddNotificationStatusSuccess_ValidNotificationStatus()
        {
            var notificationStatus = GetValidNotificationStatus();
            _notificationStatusManipulation.AddNotificationStatus(notificationStatus);
        }

        [TestMethod, TestCategory("NotificationStatus - GetNotificationStatus")]
        public void GetNotificationStatusById_ValidNotificationStatus()
        {
            var notificationStatus = GetValidNotificationStatus();
            _notificationStatusManipulation.AddNotificationStatus(notificationStatus);
            var nStatusGet = _notificationStatusManipulation.GetNotificationStatusById(2);

            Assert.AreEqual(notificationStatus.Code, nStatusGet.Code);
            Assert.AreEqual(notificationStatus.Name, nStatusGet.Name);
        }

        [TestMethod, TestCategory("NotificationStatus - GetNotificationStatus")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid notification status ID.", SeverityEnum.Warning)]

        public void GetNotificationStatusById_Fail_Negative_ID()
        {
            var notificationStatus = GetValidNotificationStatus();
            _notificationStatusManipulation.AddNotificationStatus(notificationStatus);
            var nStatusGet = _notificationStatusManipulation.GetNotificationStatusById(-1);
        }

        [TestMethod, TestCategory("NotificationStatus - GetNotificationStatus")]
        public void GetAllNotificationStatuses_Valid()
        {
            var notificationStatus = _notificationStatusManipulation.GetAllNotificationStatuses();
        }

        [TestMethod, TestCategory("NotificationStatus - GetNotificationStatus")]
        public void GetNotificationStatusByCode_Valid()
        {
            var notificationStatus = _notificationStatusManipulation.GetNotificationStatusByCode("TESTCODEEXISTS");
        }

        [TestMethod, TestCategory("NotificationStatus - GetNotificationStatus")]
        public void GetNotificationStatusByName_Valid()
        {
            var notificationStatus = _notificationStatusManipulation.GetNotificationStatusByName("Test Notification Status");
        }

        [TestMethod, TestCategory("NotificationStatus - GetNotificationStatus")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification status code not provided.", SeverityEnum.Warning)]
        public void GetNotificationStatusByCode_Fail_NullCode()
        {
            var notificationStatus = _notificationStatusManipulation.GetNotificationStatusByCode(null);
        }

        [TestMethod, TestCategory("NotificationStatus - GetNotificationStatus")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification status name not provided.", SeverityEnum.Warning)]
        public void GetNotificationStatusByName_Fail_NullName()
        {
            var notificationStatus = _notificationStatusManipulation.GetNotificationStatusByName(null);
        }

        [TestMethod, TestCategory("NotificationStatus - GetNotificationStatus")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification status name not provided.", SeverityEnum.Warning)]
        public void GetNotificationStatusByName_Fail_WhiteSpaceName()
        {
            var notificationStatus = _notificationStatusManipulation.GetNotificationStatusByName("       ");
        }

        [TestMethod, TestCategory("NotificationStatus - UpdateNotificationStatus")]
        public void UpdateNotificationStatus_Valid()
        {
            var notificationStatus = _notificationStatusManipulation.GetNotificationStatusById(2);
            notificationStatus.Code = "TestCode";
            notificationStatus.Name = "TestName";
            _notificationStatusManipulation.UpdateNotificationStatus(notificationStatus);
        }

        [TestMethod, TestCategory("NotificationStatus - UpdateNotificationStatus")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification status with provided ID does not exists.", SeverityEnum.Warning)]

        public void UpdateNotificationStatus_Fail_NotificationByIdDoesNotExists()
        {
            var notificationStatus = _notificationStatusManipulation.GetNotificationStatusById(2);
            notificationStatus.Code = "TestCode";
            notificationStatus.Name = "TestName";
            notificationStatus.Id = 100;
            _notificationStatusManipulation.UpdateNotificationStatus(notificationStatus);
        }

        [TestMethod, TestCategory("NotificationStatus - UpdateNotificationStatus")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid notification status ID.", SeverityEnum.Warning)]

        public void UpdateNotificationStatus_Fail_NegativeId()
        {
            var notificationStatus = _notificationStatusManipulation.GetNotificationStatusById(2);
            notificationStatus.Code = "TestCode";
            notificationStatus.Name = "TestName";
            notificationStatus.Id = -111;
            _notificationStatusManipulation.UpdateNotificationStatus(notificationStatus);
        }

        [TestMethod, TestCategory("NotificationStatus - DeleteNotificationStatus")]

        public void DeleteNotificationStatus_Valid()
        {
            _notificationStatusManipulation.DeleteNotificationStatus(2);
        }

        [TestMethod, TestCategory("NotificationStatus - DeleteNotificationStatus")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Notification status with provided ID does not exist.", SeverityEnum.Error)]
        public void DeleteNotificationStatus_Fail_InvalidId()
        {
            _notificationStatusManipulation.DeleteNotificationStatus(111);
        }

        [TestMethod, TestCategory("NotificationStatus - SearchNotificationStatus")]
        public void SearchNotificationStatusByCodeNonExactMach_Success()
        {
            NotificationStatusRepositoryMock.filterCriteriaList.FirstOrDefault().IsExactMatch = false;
            NotificationStatusRepositoryMock.filterCriteriaList.FirstOrDefault().FilterTerm = "NOTEXIST";
            var notificationStatus = _notificationStatusManipulation.SearchNotificationStatus(NotificationStatusRepositoryMock.paging, NotificationStatusRepositoryMock.filterCriteriaList, NotificationStatusRepositoryMock.sortCriteriaList);
            Assert.AreEqual("TESTDOESNOTEXIST", notificationStatus.FirstOrDefault().Code);
        }

        [TestMethod, TestCategory("NotificationStatus - SearchNotificationStatus")]
        public void SearchNotificationStatusByCodeExactMatch_Success()
        {
            var notificationStatus = _notificationStatusManipulation.SearchNotificationStatus(NotificationStatusRepositoryMock.paging, NotificationStatusRepositoryMock.filterCriteriaList, NotificationStatusRepositoryMock.sortCriteriaList);
            Assert.AreEqual("TESTDOESNOTEXIST", notificationStatus.FirstOrDefault().Code);
        }

        [TestMethod, TestCategory("NotificationStatus - SearchNotificationStatus")]
        public void SearchNotificationStatusByNameExactMach_Success()
        {
            var notificationStatus = _notificationStatusManipulation.SearchNotificationStatus(NotificationStatusRepositoryMock.paging, NotificationStatusRepositoryMock.filterCriteriaList, NotificationStatusRepositoryMock.sortCriteriaList);
            Assert.AreEqual("TESTDOESNOTEXIST", notificationStatus.FirstOrDefault().Code);
            Assert.AreEqual("Test Notification Status", notificationStatus.FirstOrDefault().Name);
        }

        [TestMethod, TestCategory("NotificationStatus - SearchNotificationStatus")]
        public void SearchNotificationStatusByNameNonExactMach_Success()
        {
            NotificationStatusRepositoryMock.filterCriteriaList.FirstOrDefault().IsExactMatch = false;
            NotificationStatusRepositoryMock.filterCriteriaList.FirstOrDefault().FilterTerm = "NOTEXIST";
            var notificationStatus = _notificationStatusManipulation.SearchNotificationStatus(NotificationStatusRepositoryMock.paging, NotificationStatusRepositoryMock.filterCriteriaList, NotificationStatusRepositoryMock.sortCriteriaList);
            Assert.AreEqual("TESTDOESNOTEXIST", notificationStatus.FirstOrDefault().Code);
            Assert.AreEqual("Test Notification Status", notificationStatus.FirstOrDefault().Name);
        }

        private NotificationStatusDomain GetValidNotificationStatus()
        {
            return new NotificationStatusDomain()
            {
                Code = "TESTDOESNOTEXIST",
                Name = "Test Notification Status",
                Id = 1
            };
        }
    }

}
