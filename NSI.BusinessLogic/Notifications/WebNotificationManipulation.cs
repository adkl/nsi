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
using NSI.Repository.Interfaces;

namespace NSI.BusinessLogic.Notifications
{
    public class WebNotificationManipulation : IWebNotificationManipulation
    {
        private readonly IWebNotificationRepository _webNotificationRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="webNotificationRepository"></param>
        /// <param name="notificationRepository"></param>
        /// <param name="userRepository"></param>
        public WebNotificationManipulation(IWebNotificationRepository webNotificationRepository, 
                                           INotificationRepository notificationRepository,
                                           IUserRepository userRepository)
        {
            _webNotificationRepository = webNotificationRepository;
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Add a web notification
        /// </summary>
        /// <param name="webNotification"></param>
        /// <returns></returns>
        public int AddWebNotification(WebNotificationDomain webNotification)
        {
            ValidateWebNotificationModel(webNotification);
            return _webNotificationRepository.Add(webNotification);
        }

        /// <summary>
        /// Get all web notifications
        /// </summary>
        /// <returns></returns>
        public ICollection<WebNotificationDomain> GetAllWebNotifications()
        {
            return _webNotificationRepository.GetAll();
        }

        /// <summary>
        /// Get web notification by id
        /// </summary>
        /// <param name="webNotificationId"></param>
        /// <returns></returns>
        public WebNotificationDomain GetWebNotificationById(int webNotificationId)
        {
            ValidationHelper.GreaterThanZero(webNotificationId, NotificationMessages.WebNotificationIdInvalid);
            return _webNotificationRepository.GetById(webNotificationId);
        }

        /// <summary>
        /// Update web notification
        /// </summary>
        /// <param name="webNotification"></param>
        public void UpdateWebNotification(WebNotificationDomain webNotification)
        {
            ValidateWebNotificationModel(webNotification);
            ValidationHelper.GreaterThanZero(webNotification.Id, NotificationMessages.WebNotificationIdInvalid);
            ValidationHelper.NotNull(_webNotificationRepository.GetById(webNotification.Id), NotificationMessages.WebNotificationWithIdDoesNotExist);
            _webNotificationRepository.Update(webNotification);
        }

        /// <summary>
        /// Set web notification as seen
        /// </summary>
        /// <param name="webNotifications"></param>
        public void UpdateWebNotificationsSeen(ICollection<WebNotificationDomain> webNotifications)
        {
            foreach(WebNotificationDomain webNotification in webNotifications)
            {
                ValidateWebNotificationModel(webNotification);
                ValidationHelper.GreaterThanZero(webNotification.Id, NotificationMessages.WebNotificationIdInvalid);
                ValidationHelper.NotNull(_webNotificationRepository.GetById(webNotification.Id), NotificationMessages.WebNotificationWithIdDoesNotExist);
            }
            _webNotificationRepository.Update(webNotifications);
        }


        private void ValidateWebNotificationModel(WebNotificationDomain webNotification)
        {
            ValidationHelper.NotNull(webNotification, NotificationMessages.WebNotificationNotProvided);
            ValidationHelper.GreaterThanZero(webNotification.NotificationId, NotificationMessages.WebNotificationNotificationIdInvalid);
            ValidationHelper.NotNull(_notificationRepository.GetById(webNotification.NotificationId), NotificationMessages.NotificationWithIdDoesNotExist);
            ValidationHelper.GreaterThanZero(webNotification.UserInfoId,NotificationMessages.WebNotificationUserInfoIdInvalid);
            ValidationHelper.NotNull(_userRepository.GetUserById(webNotification.UserInfoId), NotificationMessages.WebNotificationUserDoesNotExist);
        }
    }
}
