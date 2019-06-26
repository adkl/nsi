using NSI.BusinessLogic.Interfaces.Notifications;
using NSI.Common.Helpers;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces;
using NSI.Repository.Interfaces.Notifications;
using NSI.Resources.Notifications;
using System.Collections.Generic;

namespace NSI.BusinessLogic.Notifications {
    public class NotificationUserManipulation : INotificationUserManipulation {
        private readonly INotificationUserRepository _notificationUserRepository;

        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="notificationUserRepository"></param>
        /// <param name="notificationRepository"></param>
        /// <param name="userRepository"></param>
        public NotificationUserManipulation(INotificationUserRepository notificationUserRepository, INotificationRepository notificationRepository, IUserRepository userRepository) {
            _notificationUserRepository = notificationUserRepository;
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Add notification user
        /// </summary>
        /// <param name="notificationUser"></param>
        /// <returns></returns>
        public int AddNotificationUser(NotificationUserDomain notificationUser) {
            ValidateNotificationModel(notificationUser);
            return _notificationUserRepository.Add(notificationUser);
        }

        /// <summary>
        /// Update notification user
        /// </summary>
        /// <param name="notificationUser"></param>
        public void UpdateNotificationUser(NotificationUserDomain notificationUser) {
            ValidateNotificationModel(notificationUser);
            ValidationHelper.GreaterThanZero(notificationUser.Id, NotificationMessages.NotificationUserIdInvalid);
            ValidationHelper.NotNull(_notificationUserRepository.GetById(notificationUser.Id), NotificationMessages.NotificationUserWithIdDoesNotExist);

            _notificationUserRepository.Update(notificationUser);
        }

        /// <summary>
        /// Delete notification user with id
        /// </summary>
        /// <param name="notificationUserId"></param>
        public void DeleteNotificationUser(int notificationUserId) {
            ValidationHelper.GreaterThanZero(notificationUserId, NotificationMessages.NotificationUserIdInvalid);
            ValidationHelper.NotNull(_notificationUserRepository.GetById(notificationUserId), NotificationMessages.NotificationUserWithIdDoesNotExist);

            _notificationUserRepository.Delete(notificationUserId);
        }

        /// <summary>
        /// Get all notification users
        /// </summary>
        /// <returns></returns>
        public ICollection<NotificationUserDomain> GetAllNotificationUsers() {
            return _notificationUserRepository.GetAll();
        }

        /// <summary>
        /// Get notification user by id
        /// </summary>
        /// <param name="notificationUserId"></param>
        /// <returns></returns>
        public NotificationUserDomain GetNotificationUserById(int notificationUserId) {
            ValidationHelper.GreaterThanZero(notificationUserId, NotificationMessages.NotificationUserIdInvalid);
            return _notificationUserRepository.GetById(notificationUserId);
        }

        /// <summary>
        /// Get notification user by notification id
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        public ICollection<NotificationUserDomain> GetNotificationUserByNotificationId(int notificationId) {
            ValidationHelper.NotNull(notificationId, NotificationMessages.NotificationIdNotProvided);
            return _notificationUserRepository.GetByNotificationId(notificationId);
        }

        /// <summary>
        /// Get notification user by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ICollection<NotificationUserDomain> GetNotificationUserByUserId(int userId) {
            ValidationHelper.NotNull(userId, NotificationMessages.UserIdNotProvided);
            return _notificationUserRepository.GetByUserId(userId);
        }

        private void ValidateNotificationModel(NotificationUserDomain notificationUser) {
            ValidationHelper.NotNull(notificationUser, NotificationMessages.NotificationUserNotProvided);
            ValidationHelper.NotNull(notificationUser.NotificationId, NotificationMessages.NotificationIdNotProvided);
            ValidationHelper.NotNull(notificationUser.UserInfoId, NotificationMessages.UserIdNotProvided);
            ValidationHelper.NotNull(notificationUser.UserTenantId, NotificationMessages.NotificationTypeIdNotProvided);
            ValidationHelper.GreaterThanZero(notificationUser.NotificationId, NotificationMessages.NotificationIdInvalid);
            ValidationHelper.GreaterThanZero(notificationUser.UserInfoId, NotificationMessages.UserIdInvalid);
            ValidationHelper.NotNull(_notificationRepository.GetById(notificationUser.NotificationId), NotificationMessages.NotificationWithIdDoesNotExist);
            ValidationHelper.NotNull(_userRepository.GetUserById(notificationUser.UserInfoId), NotificationMessages.UserWithIdDoesNotExist);
            ValidationHelper.NotNull(_userRepository.GetUsersByTenantId(notificationUser.UserTenantId), NotificationMessages.UserWithTenantIdDoesNotExist);
        }

    }
}
