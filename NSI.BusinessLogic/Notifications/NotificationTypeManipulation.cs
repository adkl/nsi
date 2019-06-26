using NSI.BusinessLogic.Interfaces.Notifications;
using NSI.Domain.Notifications;
using NSI.Common.Helpers;
using NSI.Repository.Interfaces.Notifications;
using NSI.Resources.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Notifications
{
    public class NotificationTypeManipulation : INotificationTypeManipulation
    {
        private readonly INotificationTypeRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public NotificationTypeManipulation(INotificationTypeRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all notification types
        /// </summary>
        /// <returns></returns>
        public ICollection<NotificationTypeDomain> GetAllNotificationTypes()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Get notification type by id
        /// </summary>
        /// <param name="notificationTypeId"></param>
        /// <returns></returns>
        public NotificationTypeDomain GetNotificationTypeById(int notificationTypeId)
        {
            ValidationHelper.GreaterThanZero(notificationTypeId, NotificationMessages.NotificationTypeIdInvalid);
            return _repository.GetById(notificationTypeId);
        }

        /// <summary>
        /// Get notification type by name
        /// </summary>
        /// <param name="notificationTypeName"></param>
        /// <returns></returns>
        public NotificationTypeDomain GetNotificationTypeByName(string notificationTypeName)
        {
            ValidationHelper.NotNullOrWhitespace(notificationTypeName, NotificationMessages.NotificationTypeNameEmpty);
            return _repository.GetByName(notificationTypeName);
        }

        /// <summary>
        /// Get notification type by code
        /// </summary>
        /// <param name="notificationTypeCode"></param>
        /// <returns></returns>
        public NotificationTypeDomain GetNotificationTypeByCode(string notificationTypeCode)
        {
            ValidationHelper.NotNullOrWhitespace(notificationTypeCode, NotificationMessages.NotificationTypeCodeEmpty);
            return _repository.GetByCode(notificationTypeCode);
        }

        /// <summary>
        /// Add notification type
        /// </summary>
        /// <param name="notificationType"></param>
        /// <returns></returns>
        public int AddNotificationType(NotificationTypeDomain notificationType)
        {
            ValidateNotificationTypeModel(notificationType);

            bool codeExists = _repository.GetAllByCode(notificationType.Code).Count > 0;
            ValidationHelper.IsFalse(codeExists, NotificationMessages.NotificationTypeCodeExists);

            return _repository.Add(notificationType);
        }

        /// <summary>
        /// Update notification type
        /// </summary>
        /// <param name="notificationType"></param>
        public void UpdateNotificationType(NotificationTypeDomain notificationType)
        {
            ValidateNotificationTypeModel(notificationType);
            ValidationHelper.GreaterThanZero(notificationType.Id, NotificationMessages.NotificationTypeIdInvalid);
            ValidationHelper.NotNull(_repository.GetById(notificationType.Id), NotificationMessages.NotificationTypeWithIdDoesNotExist);

            ICollection<NotificationTypeDomain> notificationTypeList = _repository.GetAllByCode(notificationType.Code);
            bool codeExists = notificationTypeList.Count > 1 ||
                (notificationTypeList.Count == 1 && notificationTypeList.ElementAt(0).Id != notificationType.Id);
            ValidationHelper.IsFalse(codeExists, NotificationMessages.NotificationTypeCodeExists);

            _repository.Update(notificationType);
        }

        private void ValidateNotificationTypeModel(NotificationTypeDomain notificationType)
        {
            ValidationHelper.NotNull(notificationType, NotificationMessages.NotificationTypeNotProvided);
            ValidationHelper.NotNullOrWhitespace(notificationType.Name, NotificationMessages.NotificationTypeNameEmpty);
            ValidationHelper.NotNullOrWhitespace(notificationType.Code, NotificationMessages.NotificationTypeCodeEmpty);
        }
    }
}
