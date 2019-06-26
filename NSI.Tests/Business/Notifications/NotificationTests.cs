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

namespace NSI.Tests.Business.Notifications {
    [TestClass]
    public class NotificationTests {
        private Mock<INotificationRepository> _notificationRepositoryMock;
        private Mock<INotificationStatusRepository> _notificationStatusRepositoryMock;
        private Mock<INotificationTypeRepository> _notificationTypeRepositoryMock;
        private NotificationManipulation _notificationManipulation;

        [TestInitialize]
        public void Initialize() {
            _notificationRepositoryMock = NotificationRepositoryMock.GetNotificationRepositoryMock();
            _notificationStatusRepositoryMock = NotificationStatusRepositoryMock.GetNotificationStatusRepositoryMock();
            _notificationTypeRepositoryMock = NotificationTypeRepositoryMock.GetNotificationTypeRepositoryMock();
            _notificationManipulation = new NotificationManipulation(_notificationRepositoryMock.Object, _notificationStatusRepositoryMock.Object, _notificationTypeRepositoryMock.Object);
        }

        [TestMethod, TestCategory("Notification - AddNotification")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification details are not provided.", SeverityEnum.Warning)]
        public void AddNotification_Fail_ParameterNull() {
            _notificationManipulation.AddNotification(null);
        }

        [TestMethod, TestCategory("Notification - AddNotification")]
        //[ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification externalId not provided.", SeverityEnum.Warning)]
        public void AddNotification_Fail_ExternalIdNull() {
            var Notification = new NotificationDomain() {
                Id = 1,
                Content = "Test content",
                NotificationStatusId = 2,
                NotificationTypeId = 2,
                Title = "TEST",
            };

            _notificationManipulation.AddNotification(Notification);
        }

        [TestMethod, TestCategory("Notification - AddNotification")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided notification status ID not valid.", SeverityEnum.Warning)]
        public void AddNotification_Fail_NotificationStatusIdNull() {
            var Notification = new NotificationDomain() {
                Id = 1,
                Content = "Test content",
                NotificationTypeId = 2,
                ExternalId = new Guid(),
                Title = "TEST",
            };

            _notificationManipulation.AddNotification(Notification);
        }

        [TestMethod, TestCategory("Notification - AddNotification")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided notification type ID not valid.", SeverityEnum.Warning)]
        public void AddNotification_Fail_NotificationTypeIdNull() {
            var Notification = new NotificationDomain() {
                Id = 1,
                Content = "Test content",
                NotificationStatusId = 2,
                ExternalId = new Guid(),
                Title = "TEST",
            };

            _notificationManipulation.AddNotification(Notification);
        }

        [TestMethod, TestCategory("Notification - AddNotification")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided notification status ID not valid.", SeverityEnum.Warning)]
        public void AddNotification_Fail_NotificationStatusIdInvalid() {
            var Notification = GetValidNotification();
            Notification.NotificationStatusId = -1;
            _notificationManipulation.AddNotification(Notification);
        }

        [TestMethod, TestCategory("Notification - AddNotification")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided notification type ID not valid.", SeverityEnum.Warning)]
        public void AddNotification_Fail_NotificationTypeIdInvalid() {
            var Notification = GetValidNotification();
            Notification.NotificationTypeId = -1;
            _notificationManipulation.AddNotification(Notification);
        }

        [TestMethod, TestCategory("Notification - AddNotification")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification status with provided ID does not exist.", SeverityEnum.Warning)]
        public void AddNotification_Fail_NotificationStatusNotExist() {
            var Notification = GetValidNotification();
            Notification.NotificationStatusId = 555;
            _notificationManipulation.AddNotification(Notification);
        }

        [TestMethod, TestCategory("Notification - AddNotification")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification type with provided ID dooes not exist.", SeverityEnum.Warning)]
        public void AddNotification_Fail_NotificationTypeNotExist() {
            var Notification = GetValidNotification();
            Notification.NotificationTypeId = 555;
            _notificationManipulation.AddNotification(Notification);
        }

        [TestMethod, TestCategory("Notification - AddNotification")]
        public void AddNotificationSuccess_ValidNotification() {
            var notification = GetValidNotification();
            _notificationManipulation.AddNotification(notification);
        }

        [TestMethod, TestCategory("NotificationStatus - GetAllNotifications")]
        public void GetAllNotifications_Valid() {
            var notification = _notificationManipulation.GetAllNotifications();
        }

        [TestMethod, TestCategory("Notification - GetNotificationById")]
        public void GetNotificationById_ValidNotification() {
            var notification = GetValidNotification();
            _notificationManipulation.AddNotification(notification);
            var nNotificationGet = _notificationManipulation.GetNotificationById(1);

            Assert.AreEqual(notification.Title, nNotificationGet.Title);
            Assert.AreEqual(notification.Content, nNotificationGet.Content);
            Assert.AreEqual(notification.ExternalId, nNotificationGet.ExternalId);
            Assert.AreEqual(notification.NotificationStatusId, nNotificationGet.NotificationStatusId);
            Assert.AreEqual(notification.NotificationTypeId, nNotificationGet.NotificationTypeId);
        }

        [TestMethod, TestCategory("Notification - GetNotificationByExternalId")]
        public void GetNotificationByExternalId_Valid() {
            var nNotificationGet = _notificationManipulation.GetNotificationByExternalId(new Guid());
        }

        [TestMethod, TestCategory("Notification - GetNotificationByCreatedDate")]
        public void GetNotificationByCreatedDate_Valid() {
            var nNotificationGet = _notificationManipulation.GetNotificationByCreatedDate(new DateTime().Date);
        }

        [TestMethod, TestCategory("Notification - GetNotificationById")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided notification ID not valid.", SeverityEnum.Warning)]
        public void GetNotificationById_Fail_Negative_ID() {
            var notification = GetValidNotification();
            _notificationManipulation.AddNotification(notification);
            var nStatusGet = _notificationManipulation.GetNotificationById(-1);
        }

        [TestMethod, TestCategory("Notification - UpdateNotification")]
        public void UpdateNotification_Valid() {
            var notification = _notificationManipulation.GetNotificationById(1);
            notification.Title = "UpdateTitle";
            notification.Content = "UpdateContent";
            _notificationManipulation.UpdateNotification(notification);
        }

        [TestMethod, TestCategory("Notification - UpdateNotification")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification with provided ID does not exist.", SeverityEnum.Warning)]
        public void UpdateNotification_Fail_NotificationByIdDoesNotExists() {
            var notification = _notificationManipulation.GetNotificationById(1);
            notification.Title = "UpdateTitle";
            notification.Content = "UpdateContent";
            notification.Id = 100;
            _notificationManipulation.UpdateNotification(notification);
        }

        [TestMethod, TestCategory("Notification - UpdateNotification")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided notification ID not valid.", SeverityEnum.Warning)]
        public void UpdateNotification_Fail_NegativeId() {
            var notification = _notificationManipulation.GetNotificationById(1);
            notification.Title = "UpdateTitle";
            notification.Content = "UpdateContent";
            notification.Id = -1;
            _notificationManipulation.UpdateNotification(notification);
        }

        [TestMethod, TestCategory("Notification - DeleteNotification")]
        public void DeleteNotification_Valid() {
            _notificationManipulation.DeleteNotification(1);
        }

        [TestMethod, TestCategory("Notification - DeleteNotification")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification with provided ID does not exist.", SeverityEnum.Warning)]
        public void DeleteNotification_Fail_InvalidId() {
            _notificationManipulation.DeleteNotification(111);
        }

        [TestMethod, TestCategory("Notification - DeleteNotification")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided notification ID not valid.", SeverityEnum.Warning)]
        public void DeleteNotification_Fail_NegativeId() {
            _notificationManipulation.DeleteNotification(-1);
        }

        private NotificationDomain GetValidNotification() {
            return new NotificationDomain() {
                Id = 1,
                Content = "Test content",
                NotificationStatusId = 2,
                NotificationTypeId = 2,
                ExternalId = new Guid(),
                Title = "TEST",
            };
        }
    }
}
