using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks.Notifications;
using NSI.BusinessLogic.Notifications;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Tests.Business.Notifications
{
    [TestClass]
    public class NotificationTypeTests
    {
        private Mock<INotificationTypeRepository> _notificationTypeRepositoryMock;
        private NotificationTypeManipulation _notificationTypeManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _notificationTypeRepositoryMock = NotificationTypeRepositoryMock.GetNotificationTypeRepositoryMock();
            _notificationTypeManipulation = new NotificationTypeManipulation(_notificationTypeRepositoryMock.Object);
        }

        [TestMethod, TestCategory("Notification type - Get notification type")]
        public void GetAllNotificationType_Valid()
        {
            var notifiactionTypes = _notificationTypeManipulation.GetAllNotificationTypes();
            //Assert.IsNotNull(notifiactionTypes);
            //Assert.Equals(notifiactionTypes.Count, 2);
        }

        [TestMethod, TestCategory("Notification type - Get notification type")]
        public void GetNotificatinTypeById_Valid()
        {
            var notificationType = _notificationTypeManipulation.GetNotificationTypeById(2);
            Assert.IsNotNull(notificationType);
            Assert.AreEqual(2, notificationType.Id);
        }

        [TestMethod, TestCategory("Notification type - Get notification type")]
        public void GetNotificatinTypeById_Fail_IdNotInDB()
        {
            var notificationType = _notificationTypeManipulation.GetNotificationTypeById(1);
            Assert.IsNull(notificationType);
        }

        [TestMethod, TestCategory("Notification type - Get notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided notification type ID not valid.", SeverityEnum.Warning)]
        public void GetNotificatinTypeById_Fail_IdNotValid()
        {
            var notificationType = _notificationTypeManipulation.GetNotificationTypeById(-1);
        }

        [TestMethod, TestCategory("Notification type - Get notification type")]
        public void GetNotificationTypeByName_Valid()
        {
            var notificationType = _notificationTypeManipulation.GetNotificationTypeByName("TESTNAMEEXISTS");
            Assert.IsNotNull(notificationType);
            Assert.AreEqual("TESTNAMEEXISTS", notificationType.Name);
        }

        [TestMethod, TestCategory("Notification type - Get notification type")]
        public void GetNotificationTypeByName_Fail_NameNotInDB()
        {
            var notificationType = _notificationTypeManipulation.GetNotificationTypeByName("hahah");
            Assert.IsNull(notificationType);
        }

        [TestMethod, TestCategory("Notification type - Get notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification type name not provided.", SeverityEnum.Warning)]
        public void GetNotificationTypeByName_Fail_NameNotValid()
        {
            var notificationType = _notificationTypeManipulation.GetNotificationTypeByName("");
            Assert.IsNotNull(notificationType);
        }

        [TestMethod, TestCategory("Notification type - Get notification type")]
        public void GetNotificationTypeByCode_Valid()
        {
            var notificationType = _notificationTypeManipulation.GetNotificationTypeByCode("TESTCODEEXISTS");
            Assert.IsNotNull(notificationType);
            Assert.AreEqual("TESTCODEEXISTS", notificationType.Code);
        }

        [TestMethod, TestCategory("Notification type - Get notification type")]
        public void GetNotificationTypeByCode_Fail_CodeNotInDB()
        {
            var notificationType = _notificationTypeManipulation.GetNotificationTypeByCode("hahaha");
            Assert.IsNull(notificationType);
        }

        [TestMethod, TestCategory("Notification type - Get notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification type code not provided.", SeverityEnum.Warning)]
        public void GetNotificationTypeByCode_Fail_CodeNotValid()
        {
            var notificationType = _notificationTypeManipulation.GetNotificationTypeByCode("");
            Assert.IsNotNull(notificationType);
        }

        [TestMethod, TestCategory("Notification type - Add notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification type details not provided.", SeverityEnum.Warning)]
        public void AddNotificationType_Fail_RequestNull()
        {
            int notificationTypeId = _notificationTypeManipulation.AddNotificationType(null);
        }

        [TestMethod, TestCategory("Notification type - Add notification type")]
        public void AddNotificationType_Valid()
        {
            int notificationTypeId = _notificationTypeManipulation
                .AddNotificationType(new NotificationTypeDomain
                {
                    Code = "TestCodeUnique",
                    Name = "TestName"
                });
            Assert.IsNotNull(notificationTypeId);
            Assert.AreEqual(1, notificationTypeId);
        }

        [TestMethod, TestCategory("Notification type - Add notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification type name not provided.", SeverityEnum.Warning)]
        public void AddNotificationType_Fail_NameNull()
        {
            int notificationTypeId = _notificationTypeManipulation
                .AddNotificationType(new NotificationTypeDomain
                {
                    Code = "TestCode"
                });
        }

        [TestMethod, TestCategory("Notification type - Add notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification type name not provided.", SeverityEnum.Warning)]
        public void AddNotificationType_Fail_NameWhitespace()
        {
            int notificationTypeId = _notificationTypeManipulation
                .AddNotificationType(new NotificationTypeDomain
                {
                    Code = "TestCode",
                    Name = "    "
                });
        }

        [TestMethod, TestCategory("Notification type - Add notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification type code not provided.", SeverityEnum.Warning)]
        public void AddNotificationType_Fail_CodeNull()
        {

            int notificationTypeId = _notificationTypeManipulation
                .AddNotificationType(new NotificationTypeDomain
                {
                    Name = "TestName"
                });
        }

        [TestMethod, TestCategory("Notification type - Add notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification type code not provided.", SeverityEnum.Warning)]
        public void AddNotificationType_Fail_CodeWhitespace()
        {

            int notificationTypeId = _notificationTypeManipulation
                .AddNotificationType(new NotificationTypeDomain
                {
                    Code = "    ",
                    Name = "TestName"
                });
        }

        [TestMethod, TestCategory("Notification type - Add notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Notification type with sam code exists in database.", SeverityEnum.Warning)]
        public void AddNotificationType_Fail_CodeNotUnique()
        {
            int notificationTypeId = _notificationTypeManipulation
                .AddNotificationType(new NotificationTypeDomain
                {
                    Code = "TestCodeNotUnique",
                    Name = "TestName"
                });
        }

        [TestMethod, TestCategory("Notification type - Update notification type")]
        public void UpdateNotificationType_Valid()
        {
            _notificationTypeManipulation.UpdateNotificationType(new NotificationTypeDomain
            {
                Id = 2,
                Code = "TestCodeUnique",
                Name = "TestName"
            });
        }

        [TestMethod, TestCategory("Notification type - Update notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification type details not provided.", SeverityEnum.Warning)]
        public void UpdateNotificationType_Fail_RequestNull()
        {
            _notificationTypeManipulation.UpdateNotificationType(null);
        }

        [TestMethod, TestCategory("Notification type - Update notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification type name not provided.", SeverityEnum.Warning)]
        public void UpdateNotificationType_Fail_NameNull()
        {
            _notificationTypeManipulation.UpdateNotificationType(new NotificationTypeDomain
            {
                Code = "TestCode"
            });
        }

        [TestMethod, TestCategory("Notification type - Update notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification type name not provided.", SeverityEnum.Warning)]
        public void UpdateNotificationType_Fail_NameWhitespace()
        {
            _notificationTypeManipulation.UpdateNotificationType(new NotificationTypeDomain
            {
                Code = "TestCode",
                Name = "    "
            });
        }

        [TestMethod, TestCategory("Notification type - Update notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification type code not provided.", SeverityEnum.Warning)]
        public void UpdateNotificationType_Fail_CodeNull()
        {
            _notificationTypeManipulation.UpdateNotificationType(new NotificationTypeDomain
            {
                Name = "TestName"
            });
        }

        [TestMethod, TestCategory("Notification type - Update notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification type code not provided.", SeverityEnum.Warning)]
        public void UpdateNotificationType_Fail_CodeWhitespace()
        {
            _notificationTypeManipulation.UpdateNotificationType(new NotificationTypeDomain
            {
                Code = "    ",
                Name = "TestName"
            });
        }

        [TestMethod, TestCategory("Notification type - Update notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided notification type ID not valid.", SeverityEnum.Warning)]
        public void UpdateNotificationType_Fail_IdNotValid()
        {
            _notificationTypeManipulation.UpdateNotificationType(new NotificationTypeDomain
            {
                Code = "TestCode",
                Name = "TestName"
            });
        }

        [TestMethod, TestCategory("Notification type - Update notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification type with provided ID dooes not exist.", SeverityEnum.Warning)]
        public void UpdateNotificationType_Fail_IdNotInDB()
        {
            _notificationTypeManipulation.UpdateNotificationType(new NotificationTypeDomain
            {
                Id = 4,
                Code = "TestCode",
                Name = "TestName"
            });
        }

        [TestMethod, TestCategory("Notification type - Update notification type")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Notification type with sam code exists in database.", SeverityEnum.Warning)]
        public void UpdateNotificationType_Fail_CodeNotUnique()
        {
            _notificationTypeManipulation.UpdateNotificationType(new NotificationTypeDomain
            {
                Id = 2,
                Code = "TestCodeNotUnique",
                Name = "TestName"
            });
        }
    }
}
