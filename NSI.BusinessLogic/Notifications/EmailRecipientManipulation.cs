using NSI.BusinessLogic.Interfaces.Notifications;
using NSI.Common.Helpers;
using NSI.Common.Models;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.Notifications;
using NSI.Resources.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Notifications
{
    public class EmailRecipientManipulation : IEmailRecipientManipulation
    {
        private readonly IEmailRecipientRepository _emailRecipientRepository;

        private readonly IEmailMessageRepository _emailMessageRepository;
        private readonly IEmailRecipientTypeRepository _emailRecipientTypeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="emailRecipientRepository"></param>
        /// <param name="emailRecipientTypeRepository"></param>
        /// <param name="emailMessageRepository"></param>
        public EmailRecipientManipulation(IEmailRecipientRepository emailRecipientRepository, IEmailRecipientTypeRepository emailRecipientTypeRepository, IEmailMessageRepository emailMessageRepository)
        {
            _emailRecipientRepository = emailRecipientRepository;
            _emailMessageRepository = emailMessageRepository;
            _emailRecipientTypeRepository = emailRecipientTypeRepository;
        }

        /// <summary>
        /// Add an email recipient
        /// </summary>
        /// <param name="emailRecipient"></param>
        /// <returns></returns>
        public int AddEmailRecipient(EmailRecipientDomain emailRecipient)
        {
            ValidateEmailRecipientModel(emailRecipient);
            return _emailRecipientRepository.Add(emailRecipient);
        }

        /// <summary>
        /// Get all email recipients
        /// </summary>
        /// <returns></returns>
        public ICollection<EmailRecipientDomain> GetAllEmailRecipients()
        {
            return _emailRecipientRepository.GetAll();
        }

        /// <summary>
        /// Get email recipient with id
        /// </summary>
        /// <param name="emailRecipientId"></param>
        /// <returns></returns>
        public EmailRecipientDomain GetEmailRecipientById(int emailRecipientId)
        {
            ValidationHelper.GreaterThanZero(emailRecipientId, NotificationMessages.EmailRecipientIdInvalid);
            return _emailRecipientRepository.GetById(emailRecipientId);
        }

        /// <summary>
        /// Search email recipients by search and filter criteria
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="filterCriteria"></param>
        /// <param name="sortCriteria"></param>
        /// <returns></returns>
        public ICollection<EmailRecipientDomain> SearchEmailRecipients(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            paging.ValidatePagingCriteria();
            filterCriteria?.ToList().ForEach(x => x.ValidateFilterCriteria());
            sortCriteria?.ToList().ForEach(x => x.ValidateSortCriteria());
            return _emailRecipientRepository.SearchEmailRecipients(paging, filterCriteria, sortCriteria);
        }

        /// <summary>
        /// Update an email recipient
        /// </summary>
        /// <param name="emailRecipient"></param>
        public void UpdateEmailRecipient(EmailRecipientDomain emailRecipient)
        {
            ValidationHelper.NotNull(emailRecipient, NotificationMessages.EmailRecipientNotProvided);
            ValidationHelper.NotNullOrWhitespace(emailRecipient.EmailAddress, NotificationMessages.EmailRecipientAddressEmpty);
            ValidationHelper.IsEmailValid(emailRecipient.EmailAddress, NotificationMessages.EmailRecipientAddressInvalid);
            ValidationHelper.GreaterThanZero(emailRecipient.Id, NotificationMessages.EmailRecipientIdInvalid);
            ValidationHelper.NotNull(_emailRecipientRepository.GetById(emailRecipient.Id), NotificationMessages.EmailRecipientWithIdDoesNotExist);                      
                                
            _emailRecipientRepository.Update(emailRecipient);
        }

        /// <summary>
        /// Delete email recipient with id
        /// </summary>
        /// <param name="emailRecipientId"></param>
        public void DeleteEmailRecipient(int emailRecipientId)
        {           
            ValidationHelper.GreaterThanZero(emailRecipientId, NotificationMessages.EmailRecipientIdInvalid);
            ValidationHelper.NotNull(_emailRecipientRepository.GetById(emailRecipientId), NotificationMessages.EmailRecipientWithIdDoesNotExist);

            _emailRecipientRepository.Delete(emailRecipientId);
        }

        private void ValidateEmailRecipientModel(EmailRecipientDomain emailRecipient)
        {
            ValidationHelper.NotNull(emailRecipient, NotificationMessages.EmailRecipientNotProvided);
            ValidationHelper.NotNullOrWhitespace(emailRecipient.EmailAddress, NotificationMessages.EmailRecipientAddressEmpty);
            ValidationHelper.IsEmailValid(emailRecipient.EmailAddress, NotificationMessages.EmailRecipientAddressInvalid);
            ValidationHelper.GreaterThanZero(emailRecipient.EmaiMessagelId, NotificationMessages.EmailMessageIdInvalid);
            ValidationHelper.NotNull(_emailMessageRepository.GetById(emailRecipient.EmaiMessagelId), NotificationMessages.EmailMessageWithIdDoesNotExist);
            ValidationHelper.GreaterThanZero(emailRecipient.EmailRecipientTypeId, NotificationMessages.EmailRecipientTypeIdInvalid);
            ValidationHelper.NotNull(_emailRecipientTypeRepository.GetById(emailRecipient.EmailRecipientTypeId), NotificationMessages.EmailRecipientTypeWithIdDoesNotExist);
        }
    }
}
