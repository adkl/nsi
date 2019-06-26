using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks.Notifications;
using NSI.BusinessLogic.Notifications;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces;
using NSI.Repository.Interfaces.Notifications;
using System.Collections.Generic;

namespace NSI.Tests.Business.Notifications
{
    [TestClass]
    public class WebNotificationTest
    {
        private Mock<IWebNotificationRepository> _webNotificationRepositoryMock;
        private Mock<INotificationRepository> _notificationRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private WebNotificationManipulation _webNotificationManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _webNotificationRepositoryMock = WebNotificationRepositoryMock.GetWebNotificationRepositoryMock();
            _notificationRepositoryMock = NotificationRepositoryMock.GetNotificationRepositoryMock();
            _userRepositoryMock = UserRepositoryMock.GetUserRepositoryMock();
            _webNotificationManipulation = new WebNotificationManipulation(_webNotificationRepositoryMock.Object, _notificationRepositoryMock.Object, _userRepositoryMock.Object);
        }

        [TestMethod, TestCategory("Web notification - Get web notification")]
        public void GetAllWebNotification_Valid()
        {
            var webNotifications = _webNotificationManipulation.GetAllWebNotifications();
        }

        [TestMethod, TestCategory("Web notification - Get web notification")]
        public void GetWebNotificationById_Valid()
        {
            var webNotification = _webNotificationManipulation.GetWebNotificationById(1);
            Assert.IsNotNull(webNotification);
            Assert.AreEqual(1, webNotification.Id);
        }

        [TestMethod, TestCategory("Web notification - Get web notification")]
        public void GetWebNotificationById_Fail_IdNotInDB()
        {
            var webNotification = _webNotificationManipulation.GetWebNotificationById(2);
            Assert.IsNull(webNotification);
        }

        [TestMethod, TestCategory("Web notification - Get web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided web notification ID not valid.", SeverityEnum.Warning)]
        public void GetWebNotificationById_Fail_IdNotValid()
        {
            var webNotification = _webNotificationManipulation.GetWebNotificationById(0);
        }

        [TestMethod, TestCategory("Web notification - Add web notification")]
        public void AddWebNotification_Valid()
        {
            int webNotificationId = _webNotificationManipulation.AddWebNotification(new WebNotificationDomain
            {
                NotificationId = 1,
                UserInfoId = 1
            });
            Assert.AreEqual(1, webNotificationId);
        }

        [TestMethod, TestCategory("Web notification - Add web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Web notification details not provided.", SeverityEnum.Warning)]
        public void AddWebNotification_Fail_RequestNull()
        {
            int webNotificationId = _webNotificationManipulation.AddWebNotification(null);
        }

        [TestMethod, TestCategory("Web notification - Add web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided notification ID for web notification is not valid.", SeverityEnum.Warning)]
        public void AddWebNotification_Fail_NotificationIdNotValid()
        {
            int webNotificationId = _webNotificationManipulation.AddWebNotification(new WebNotificationDomain
            {
                UserInfoId = 1
            });
        }

        [TestMethod, TestCategory("Web notification - Add web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification with provided ID does not exist.", SeverityEnum.Warning)]
        public void AddWebNotification_Fail_NotificationIdNotInDB()
        {
            int webNotificationId = _webNotificationManipulation.AddWebNotification(new WebNotificationDomain
            {
                NotificationId = 2,
                UserInfoId = 1
            });
        }

        [TestMethod, TestCategory("Web notification - Add web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided ID for user in web notification request is invalid.", SeverityEnum.Warning)]
        public void AddWebNotification_Fail_UserIdNotValid()
        {
            int webNotificationId = _webNotificationManipulation.AddWebNotification(new WebNotificationDomain
            {
                NotificationId = 1
            });
        }

        [TestMethod, TestCategory("Web notification - Add web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "User with provided ID in web notification request does not exist.", SeverityEnum.Warning)]
        public void AddWebNotification_Fail_UserIdNotInDB()
        {
            int webNotificationId = _webNotificationManipulation.AddWebNotification(new WebNotificationDomain
            {
                NotificationId = 1,
                UserInfoId = 2
            });
        }

        [TestMethod, TestCategory("Web notification - Update web notification")]
        public void UpdateWebNotification_Valid()
        {
            _webNotificationManipulation.UpdateWebNotification(new WebNotificationDomain
            {
                Id = 1,
                NotificationId = 1,
                UserInfoId = 1
            });
        }

        [TestMethod, TestCategory("Web notification - Update web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Web notification details not provided.", SeverityEnum.Warning)]
        public void UpdateWebNotification_Fail_RequestNull()
        {
            _webNotificationManipulation.UpdateWebNotification(null);
        }

        [TestMethod, TestCategory("Web notification - Update web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided notification ID for web notification is not valid.", SeverityEnum.Warning)]
        public void UpdateWebNotification_Fail_NotificationIdNotValid()
        {
            _webNotificationManipulation.UpdateWebNotification(new WebNotificationDomain
            {
                Id = 1,
                UserInfoId = 1
            });
        }

        [TestMethod, TestCategory("Web notification - Update web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification with provided ID does not exist.", SeverityEnum.Warning)]
        public void UpdateWebNotification_Fail_NotificationIdNotInDB()
        {
            _webNotificationManipulation.UpdateWebNotification(new WebNotificationDomain
            {
                Id = 1,
                NotificationId = 2,
                UserInfoId = 1
            });
        }

        [TestMethod, TestCategory("Web notification - Update web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided ID for user in web notification request is invalid.", SeverityEnum.Warning)]
        public void UpdateWebNotification_Fail_UserIdNotValid()
        {
            _webNotificationManipulation.UpdateWebNotification(new WebNotificationDomain
            {
                Id = 1,
                NotificationId = 1
            });
        }

        [TestMethod, TestCategory("Web notification - Update web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "User with provided ID in web notification request does not exist.", SeverityEnum.Warning)]
        public void UpdateWebNotification_Fail_UserIdNotInDB()
        {
            _webNotificationManipulation.UpdateWebNotification(new WebNotificationDomain
            {
                Id = 1,
                NotificationId = 1,
                UserInfoId = 2
            });
        }

        [TestMethod, TestCategory("Web notification - Update web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided web notification ID not valid.", SeverityEnum.Warning)]
        public void UpdateWebNotification_Fail_IdNotValid()
        {
            _webNotificationManipulation.UpdateWebNotification(new WebNotificationDomain
            {
                NotificationId = 1,
                UserInfoId = 1
            });
        }

        [TestMethod, TestCategory("Web notification - Update web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Web notification with provided ID does not exist.", SeverityEnum.Warning)]
        public void UpdateWebNotification_Fail_IdNotInDB()
        {
            _webNotificationManipulation.UpdateWebNotification(new WebNotificationDomain
            {
                Id = 2,
                NotificationId = 1,
                UserInfoId = 1
            });
        }

        [TestMethod, TestCategory("Web notification - Update web notification")]
        public void UpdateWebNotificationsSeen_Valid()
        {
            _webNotificationManipulation.UpdateWebNotificationsSeen(new List<WebNotificationDomain> {
                new WebNotificationDomain
                {
                    Id = 1,
                    NotificationId = 1,
                    UserInfoId = 1
                }
            });
        }

        [TestMethod, TestCategory("Web notification - Update web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Web notification details not provided.", SeverityEnum.Warning)]
        public void UpdateWebNotificationsSeen_Fail_RequestNull()
        {
            _webNotificationManipulation.UpdateWebNotificationsSeen(new List<WebNotificationDomain> { null });
        }

        [TestMethod, TestCategory("Web notification - Update web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided notification ID for web notification is not valid.", SeverityEnum.Warning)]
        public void UpdateWebNotificationsSeen_Fail_NotificationIdNotValid()
        {
            _webNotificationManipulation.UpdateWebNotificationsSeen(new List<WebNotificationDomain> {
                new WebNotificationDomain
                {
                    Id = 1,
                    UserInfoId = 1
                }
            });
        }

        [TestMethod, TestCategory("Web notification - Update web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification with provided ID does not exist.", SeverityEnum.Warning)]
        public void UpdateWebNotificationsSeen_Fail_NotificationIdNotInDB()
        {
            _webNotificationManipulation.UpdateWebNotificationsSeen(new List<WebNotificationDomain> {
                new WebNotificationDomain
                {
                    Id = 1,
                    NotificationId = 2,
                    UserInfoId = 1
                }
            });
        }

        [TestMethod, TestCategory("Web notification - Update web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided ID for user in web notification request is invalid.", SeverityEnum.Warning)]
        public void UpdateWebNotificationsSeen_Fail_UserIdNotValid()
        {
            _webNotificationManipulation.UpdateWebNotificationsSeen(new List<WebNotificationDomain> {
                new WebNotificationDomain
                {
                    Id = 1,
                    NotificationId = 1
                }
            });
        }

        [TestMethod, TestCategory("Web notification - Update web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "User with provided ID in web notification request does not exist.", SeverityEnum.Warning)]
        public void UpdateWebNotificationsSeen_Fail_UserIdNotInDB()
        {
            _webNotificationManipulation.UpdateWebNotificationsSeen(new List<WebNotificationDomain> {
                new WebNotificationDomain
                {
                    Id = 1,
                    NotificationId = 1,
                    UserInfoId = 2
                }
            });
        }

        [TestMethod, TestCategory("Web notification - Update web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided web notification ID not valid.", SeverityEnum.Warning)]
        public void UpdateWebNotificationsSeen_Fail_IdNotValid()
        {
            _webNotificationManipulation.UpdateWebNotificationsSeen(new List<WebNotificationDomain> {
                new WebNotificationDomain
                {
                    NotificationId = 1,
                    UserInfoId = 1
                }
            });
        }

        [TestMethod, TestCategory("Web notification - Update web notification")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Web notification with provided ID does not exist.", SeverityEnum.Warning)]
        public void UpdateWebNotificationsSeen_Fail_IdNotInDB()
        {
            _webNotificationManipulation.UpdateWebNotificationsSeen(new List<WebNotificationDomain> {
                new WebNotificationDomain
                {
                    Id = 2,
                    NotificationId = 1,
                    UserInfoId = 1
                }
            });
        }
    }
}
