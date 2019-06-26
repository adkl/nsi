using NSI.BusinessLogic.Interfaces.Notifications;
using NSI.Common.Helpers;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.Notifications;
using NSI.Resources.Notifications;
using System;
using System.Collections.Generic;

namespace NSI.BusinessLogic.Notifications {
    public class NotificationManipulation : INotificationManipulation {
        private readonly INotificationRepository _notificationRepository;

        private readonly INotificationStatusRepository _notificationStatusRepository;
        private readonly INotificationTypeRepository _notificationTypeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="notificationRepository"></param>
        /// <param name="notificationStatusRepository"></param>
        /// <param name="notificationTypeRepository"></param>
        public NotificationManipulation(INotificationRepository notificationRepository, INotificationStatusRepository notificationStatusRepository, INotificationTypeRepository notificationTypeRepository) 
        {
            _notificationRepository = notificationRepository;
            _notificationStatusRepository = notificationStatusRepository;
            _notificationTypeRepository = notificationTypeRepository;

        }

        /// <summary>
        /// Add a notification
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public int AddNotification(NotificationDomain notification) {
            ValidateNotificationModel(notification);
            return _notificationRepository.Add(notification);
        }

        /// <summary>
        /// Update notification
        /// </summary>
        /// <param name="notification"></param>
        public void UpdateNotification(NotificationDomain notification) {
            ValidateNotificationModel(notification);
            ValidationHelper.GreaterThanZero(notification.Id, NotificationMessages.NotificationIdInvalid);
            ValidationHelper.NotNull(_notificationRepository.GetById(notification.Id), NotificationMessages.NotificationWithIdDoesNotExist);

            _notificationRepository.Update(notification);
        }

        /// <summary>
        /// Delete notification with id
        /// </summary>
        /// <param name="notificationId"></param>
        public void DeleteNotification(int notificationId) {
            ValidationHelper.GreaterThanZero(notificationId, NotificationMessages.NotificationIdInvalid);
            ValidationHelper.NotNull(_notificationRepository.GetById(notificationId), NotificationMessages.NotificationWithIdDoesNotExist);

            _notificationRepository.Delete(notificationId);
        }

        /// <summary>
        /// Get all notifications
        /// </summary>
        /// <returns></returns>
        public ICollection<NotificationDomain> GetAllNotifications() {
            return _notificationRepository.GetAll();
        }

        /// <summary>
        /// Get notification by id
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        public NotificationDomain GetNotificationById(int notificationId) {
            ValidationHelper.GreaterThanZero(notificationId, NotificationMessages.NotificationIdInvalid);
            return _notificationRepository.GetById(notificationId);
        }

        /// <summary>
        /// Get notification by external id
        /// </summary>
        /// <param name="externalId"></param>
        /// <returns></returns>
        public ICollection<NotificationDomain> GetNotificationByExternalId(Guid externalId) {
            ValidationHelper.NotNull(externalId, NotificationMessages.NotificationExternalIdNotProvided);
            return _notificationRepository.GetByExternalId(externalId);
        }

        /// <summary>
        /// Get notification by date created
        /// </summary>
        /// <param name="createdDate"></param>
        /// <returns></returns>
        public ICollection<NotificationDomain> GetNotificationByCreatedDate(DateTime createdDate) {
            ValidationHelper.NotNull(createdDate, NotificationMessages.NotificationDateCreatedNotProvided);
            return _notificationRepository.GetByCreatedDate(createdDate);
        }

        private void ValidateNotificationModel(NotificationDomain notification) {
            ValidationHelper.NotNull(notification, NotificationMessages.NotificationNotProvided);
            ValidationHelper.NotNull(notification.ExternalId, NotificationMessages.NotificationExternalIdNotProvided);
            ValidationHelper.NotNull(notification.NotificationStatusId, NotificationMessages.NotificationStatusIdNotProvided);
            ValidationHelper.NotNull(notification.NotificationTypeId, NotificationMessages.NotificationTypeIdNotProvided);
            ValidationHelper.GreaterThanZero(notification.NotificationStatusId, NotificationMessages.EmailNotificationStatusIdInvalid);
            ValidationHelper.GreaterThanZero(notification.NotificationTypeId, NotificationMessages.NotificationTypeIdInvalid);
            ValidationHelper.NotNull(_notificationStatusRepository.GetById(notification.NotificationStatusId), NotificationMessages.NotificationStatusIdDoesNotExist);
            ValidationHelper.NotNull(_notificationTypeRepository.GetById(notification.NotificationTypeId), NotificationMessages.NotificationTypeWithIdDoesNotExist);
        }
    }

}
