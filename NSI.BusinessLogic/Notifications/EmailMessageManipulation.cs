using NSI.BusinessLogic.Interfaces.Notifications;
using NSI.Common.Exceptions;
using NSI.Common.Helpers;
using NSI.Common.Models;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.Notifications;
using NSI.Resources.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NSI.BusinessLogic.Notifications
{
    public class EmailMessageManipulation : IEmailMessageManipulation 
    {
        private readonly IEmailMessageRepository _emailMessageRepository;
        private readonly INotificationRepository _notificationRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="emailMessageRepository"></param>
        /// <param name="notificationRepository"></param>
        public EmailMessageManipulation(IEmailMessageRepository emailMessageRepository, INotificationRepository notificationRepository) 
        {
            _emailMessageRepository = emailMessageRepository;
            _notificationRepository = notificationRepository;
        }
        /// <summary>
        /// Add an email message
        /// </summary>
        /// <param name="emailMessage"></param>
        /// <returns></returns>
        public int AddEmailMessage(EmailMessageDomain emailMessage)
        {
            ValidateEmailMessageModel(emailMessage);

            return _emailMessageRepository.Add(emailMessage);
        }

        /// <summary>
        /// Delete email message with id
        /// </summary>
        /// <param name="emailMessageId"></param>
        public void DeleteEmailMessageById(int emailMessageId)
        {
            EmailMessageDomain emailMessageDomain = _emailMessageRepository.GetById(emailMessageId);
            if (emailMessageDomain == null)
                throw new NsiArgumentException(NotificationMessages.EmailMessageWithIdDoesNotExist);

            _emailMessageRepository.DeleteById(emailMessageId);
        }

        /// <summary>
        /// Get all email messages
        /// </summary>
        /// <returns></returns>
        public ICollection<EmailMessageDomain> GetAllEmailMessages()
        {
            return _emailMessageRepository.GetAll();
        }

        /// <summary>
        /// Get email message by id
        /// </summary>
        /// <param name="emailMessageId"></param>
        /// <returns></returns>
        public EmailMessageDomain GetEmailMessageById(int emailMessageId)
        {
            ValidationHelper.GreaterThanZero(emailMessageId, NotificationMessages.EmailMessageIdInvalid);

            return _emailMessageRepository.GetById(emailMessageId);
        }

        /// <summary>
        /// Get email message by notification id
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        public ICollection<EmailMessageDomain> GetByNotificationId(int notificationId)
        {
            return _emailMessageRepository.GetByNotificationId(notificationId);
        }

        /// <summary>
        /// Search email messages by search and filter criteria
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="filterCriteria"></param>
        /// <param name="sortCriteria"></param>
        /// <returns></returns>
        public ICollection<EmailMessageDomain> SearchEmailMessages(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            paging.ValidatePagingCriteria();
            return _emailMessageRepository.SearchEmailMessages(paging, filterCriteria, sortCriteria);
        }

        /// <summary>
        /// Update an email message
        /// </summary>
        /// <param name="emailMessage"></param>
        public void UpdateEmailMessage(EmailMessageDomain emailMessage)
        {
            
            ValidateEmailMessageModel(emailMessage);
            ValidationHelper.GreaterThanZero(emailMessage.Id, NotificationMessages.EmailMessageIdInvalidFormat);
            ValidationHelper.NotNull(_emailMessageRepository.GetById(emailMessage.Id), NotificationMessages.EmailMessageWithIdDoesNotExist);

            _emailMessageRepository.Update(emailMessage);
        }


        private void ValidateEmailMessageModel(EmailMessageDomain emailMessage)
        {
            ValidationHelper.NotNull(emailMessage, NotificationMessages.EmailMessageNotProvided);
            ValidationHelper.GreaterThanZero(emailMessage.NotificationId, NotificationMessages.EmailMessageInvalidNotificationId);
            ValidationHelper.NotNull(_notificationRepository.GetById(emailMessage.NotificationId), NotificationMessages.EmailMessageNotificationWithIdDoesNotExists);
            ValidationHelper.IsEmailValid(emailMessage.From, NotificationMessages.EmailMessageInvalidFromFormat);
        }
    }
}
