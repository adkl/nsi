using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks;
using Nsi.TestsCore.Mocks.Notifications;
using NSI.BusinessLogic.Notifications;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces;
using NSI.Repository.Interfaces.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Tests.Business.Notifications
{
    [TestClass]
    public class NotificationUserTest
    {
        private Mock<INotificationUserRepository> _notificationUserRepositoryMock;
        private Mock<INotificationRepository> _notificationRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private NotificationUserManipulation _notificationUserManipulation;

        [TestInitialize]
        public void Initialize()
        {

            _notificationRepositoryMock = Nsi.TestsCore.Mocks.Notifications.NotificationRepositoryMock.GetNotificationRepositoryMock();
            _notificationUserRepositoryMock = NotificationUserMock.GetNotificationUserRepositoryMock();
            _userRepositoryMock = UserRepositoryMock.GetUserRepositoryMock();
            _notificationUserManipulation = new NotificationUserManipulation(_notificationUserRepositoryMock.Object, _notificationRepositoryMock.Object, _userRepositoryMock.Object);
        }

        /*
         * 
         * ADD NOTIFICATIONUSERS TESTS
         * 
         */

        [TestMethod, TestCategory("NotificationUser - AddNotificationUser")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification user is not provided.", SeverityEnum.Warning)]
        public void AddNotificationUser_Fail_ParameterNull()
        {
            _notificationUserManipulation.AddNotificationUser(null);
        }

        [TestMethod, TestCategory("NotificationUser - AddNotificationUser")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "User with provided ID does not exists.", SeverityEnum.Warning)]
        public void AddNotificationUser_Fail_UserDoesNotExists()
        {
            var notificationUser = GetValidNotificationUser();
            notificationUser.UserInfoId = 9099;
            _notificationUserManipulation.AddNotificationUser(notificationUser);
        }

        [TestMethod, TestCategory("NotificationUser - AddNotificationUser")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification with provided ID does not exist.", SeverityEnum.Warning)]
        public void AddNotificationUser_Fail_NotificationDoesNotExist()
        {
            var notificationUser = GetValidNotificationUser();
            notificationUser.NotificationId = 9099;
            _notificationUserManipulation.AddNotificationUser(notificationUser);
        }

        [TestMethod, TestCategory("NotificationUser - AddNotificationUser")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided user ID not valid.", SeverityEnum.Warning)]
        public void AddNotificationUser_Fail_UserIdNull()
        {
            var notificationUser = GetValidNotificationUser();
            notificationUser.UserInfoId = -5;
            _notificationUserManipulation.AddNotificationUser(notificationUser);
        }

        [TestMethod, TestCategory("NotificationUser - AddNotificationUser")]
        public void AddNotificationUser_Success()
        {
            var notificationUser = GetValidNotificationUser();
            _notificationUserManipulation.AddNotificationUser(notificationUser);
        }
        /*
         * 
         * GET NOTIFICATIONUSERS TESTS
         * 
         */
        [TestMethod, TestCategory("NotificationUser - GetNotificationUser")]
        public void GetNotificationUser_Success_GetAll ()
        {
            var notificationUser = _notificationUserManipulation.GetAllNotificationUsers();
            Assert.IsNotNull(notificationUser.FirstOrDefault());
        }

        [TestMethod, TestCategory("NotificationUser - GetNotificationUser")]
        public void GetNotificationUser_Success_GetById()
        {
            var notificationUser = _notificationUserManipulation.GetNotificationUserById(1);
            Assert.IsNotNull(notificationUser);
        }

        [TestMethod, TestCategory("NotificationUser - GetNotificationUser")]
        public void GetNotificationUser_Success_GetByUserId()
        {
            var notificationUser = _notificationUserManipulation.GetNotificationUserByUserId(1);
            Assert.IsNotNull(notificationUser.FirstOrDefault());
        }

        [TestMethod, TestCategory("NotificationUser - GetNotificationUser")]
        public void GetNotificationUser_Success_GetByNotificationId()
        {
            var notificationUser = _notificationUserManipulation.GetNotificationUserByNotificationId(1);
            Assert.IsNotNull(notificationUser.FirstOrDefault());
        }

        /*
        * 
        * Delete NOTIFICATIONUSERS TESTS
        * 
        */

        [TestMethod, TestCategory("NotificationUser - DeleteNotificationUser")]
        public void DeleteNotificationUser_Success()
        {
            _notificationUserManipulation.DeleteNotificationUser(1);
        }

        [TestMethod, TestCategory("NotificationUser - DeleteNotificationUser")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided notification user ID not valid.", SeverityEnum.Warning)]

        public void DeleteNotificationUser_Fail_InvalidId()
        {
            _notificationUserManipulation.DeleteNotificationUser(-1);
        }

        /*
        * 
        * Update NOTIFICATIONUSERS TESTS
        * 
        */
        [TestMethod, TestCategory("NotificationUser - UpdateNotificationUser")]

        public void UpdateNotificationUser_Success()
        {
            var notificationUser = GetValidNotificationUser();
            _notificationUserManipulation.UpdateNotificationUser(notificationUser);
        }


        [TestMethod, TestCategory("NotificationUser - UpdateNotificationUser")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "User with provided tenant ID does not exists.", SeverityEnum.Warning)]

        public void UpdateNotificationUser_Fail_UserWithTenantIdDoesNotExists()
        {
            var notificationUser = GetValidNotificationUser();
            notificationUser.UserTenantId = 100;
            _notificationUserManipulation.UpdateNotificationUser(notificationUser);
        }

        private NotificationUserDomain GetValidNotificationUser()
        {
            return new NotificationUserDomain()
            {
                Id = 1,
                NotificationId = 1,
                TenantId = 1,
                UserInfoId = 1,
                UserTenantId = 1
            };
        }
    }
}
