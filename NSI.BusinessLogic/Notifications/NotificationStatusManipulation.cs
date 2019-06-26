using NSI.BusinessLogic.Interfaces.Notifications;
using NSI.Common.Exceptions;
using NSI.Common.Extensions;
using NSI.Common.Helpers;
using NSI.Common.Models;
using NSI.Domain.Notifications;
using NSI.EF;
using NSI.Repository.Extensions.Notifications;
using NSI.Repository.Interfaces.Notifications;
using NSI.Resources.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Notifications
{
    public class NotificationStatusManipulation : INotificationStatusManipulation
    {
        private readonly INotificationStatusRepository _notificationStatusRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="notificationStatusManipulation"></param>
        public NotificationStatusManipulation(INotificationStatusRepository notificationStatusManipulation)
        {
            _notificationStatusRepository = notificationStatusManipulation;
        }

        /// <summary>
        /// Add a notification status
        /// </summary>
        /// <param name="notificationStatus"></param>
        /// <returns></returns>
        public int AddNotificationStatus(NotificationStatusDomain notificationStatus)
        {
            ValidateNotificationStatus(notificationStatus);

            var notificationStatusWithProvidedCode = _notificationStatusRepository.GetByCode(notificationStatus.Code.SafeTrim());

            if (notificationStatusWithProvidedCode != null)
            {
                throw new NsiArgumentException(NotificationMessages.NotificationStatusWCodeAlreadyExists, Common.Enumerations.SeverityEnum.Warning);
            }

            return _notificationStatusRepository.AddNotificationStatus(notificationStatus);
        }

        /// <summary>
        /// Update notification status
        /// </summary>
        /// <param name="notificationStatusDomain"></param>
        public void UpdateNotificationStatus(NotificationStatusDomain notificationStatusDomain)
        {
            ValidationHelper.GreaterThanZero(notificationStatusDomain.Id, NotificationMessages.NotificationStatusInvalidId);
            ValidateNotificationStatus(notificationStatusDomain);
            ValidationHelper.NotNull(_notificationStatusRepository.GetById(notificationStatusDomain.Id), NotificationMessages.NotificationStatusWithIdDoesNotExists);
            _notificationStatusRepository.UpdateNotificationStatus(notificationStatusDomain);
        }

        /// <summary>
        /// Delete notification status with id
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteNotificationStatus(int Id)
        {
            NotificationStatusDomain notificationStatusDomain = _notificationStatusRepository.GetById(Id);
            if (notificationStatusDomain == null)
                throw new NsiArgumentException(NotificationMessages.NotificationStatusIdDoesNotExist);
            _notificationStatusRepository.DeleteNotificationStatus(notificationStatusDomain);
        }

        /// <summary>
        /// Get all notification statuses
        /// </summary>
        /// <returns></returns>
        public ICollection<NotificationStatusDomain> GetAllNotificationStatuses()
        {
            return _notificationStatusRepository.GetAll();
        }

        /// <summary>
        /// Search for notification status with search and filter criteria
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="filterCriteria"></param>
        /// <param name="sortCriteria"></param>
        /// <returns></returns>
        public ICollection<NotificationStatusDomain> SearchNotificationStatus(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            paging.ValidatePagingCriteria();
            filterCriteria?.ToList().ForEach(x => x.ValidateFilterCriteria());
            sortCriteria?.ToList().ForEach(x => x.ValidateSortCriteria());
            return _notificationStatusRepository.SearchNotificationStatus(paging, filterCriteria, sortCriteria);
        }

        /// <summary>
        /// Get notification status by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public NotificationStatusDomain GetNotificationStatusByCode(string code)
        {
            ValidationHelper.NotNullOrWhitespace(code, NotificationMessages.NotificationStatusCodeNotProvided);
            return _notificationStatusRepository.GetByCode(code);
        }

        /// <summary>
        /// Get notification status by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NotificationStatusDomain GetNotificationStatusById(int id)
        {
            ValidationHelper.GreaterThanZero(id, NotificationMessages.NotificationStatusInvalidId);
            return _notificationStatusRepository.GetById(id);
        }

        /// <summary>
        /// Get notification status by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public NotificationStatusDomain GetNotificationStatusByName(string name)
        {
            ValidationHelper.NotNullOrWhitespace(name, NotificationMessages.NotificationStatusNameNotProvided);
            return _notificationStatusRepository.GetByName(name);
        }

        private void ValidateNotificationStatus(NotificationStatusDomain notificationStatusDomain)
        {
            ValidationHelper.NotNull(notificationStatusDomain, NotificationMessages.NotificationStatusNotProvided);
            ValidationHelper.NotNullOrWhitespace(notificationStatusDomain.Code, NotificationMessages.NotificationStatusCodeNotProvided);
            ValidationHelper.NotNullOrWhitespace(notificationStatusDomain.Name, NotificationMessages.NotificationStatusNameNotProvided);
        }
    }
}
