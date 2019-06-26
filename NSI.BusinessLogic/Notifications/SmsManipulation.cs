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
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Notifications
{
    public class SmsManipulation : ISmsManipulation
    {
        private readonly ISmsRepository _smsRepository;
        private readonly INotificationRepository _notificationRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="smsRepository"></param>
        /// <param name="notificationRepository"></param>
        public SmsManipulation(ISmsRepository smsRepository, INotificationRepository notificationRepository)
        {
            _smsRepository = smsRepository;
            _notificationRepository = notificationRepository;
        }

        /// <summary>
        /// Add an sms
        /// </summary>
        /// <param name="sms"></param>
        /// <returns></returns>
        public int AddSms(SmsDomain sms)
        {
            ValidateSmsModel(sms);
            return _smsRepository.Add(sms);
        }

        /// <summary>
        /// Delete sms
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteSms(int Id)
        {
            SmsDomain smsDomain = _smsRepository.GetById(Id);
            if (smsDomain == null)
                throw new NsiArgumentException(NotificationMessages.SmsWithIdDoesNotExists);
            _smsRepository.Delete(smsDomain);
        }

        /// <summary>
        /// Get sms by notification id
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        public ICollection<SmsDomain> GetByNotificationId(int notificationId)
        {
            return _smsRepository.GetByNotificationId(notificationId);
        }

        /// <summary>
        /// Get sms by from number
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public SmsDomain GetSmsByFrom(string from)
        {
            ValidationHelper.IsPhoneNumberValid(from, NotificationMessages.SmsInvalidSenderPhoneNumberFormat);
            return _smsRepository.GetByFrom(from);
        }

        /// <summary>
        /// Get sms by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SmsDomain GetSmsById(int id)
        {
            ValidationHelper.GreaterThanZero(id, NotificationMessages.SmsInvalidId);
            return _smsRepository.GetById(id);
        }

        /// <summary>
        /// Get sms by phone number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public SmsDomain GetSmsByPhoneNumber(string number)
        {
            ValidationHelper.IsPhoneNumberValid(number, NotificationMessages.SmsInvalidPhoneNumberFormat);
            return _smsRepository.GetByPhoneNumber(number);
        }

        /// <summary>
        /// Search for sms by search and filter criteria
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="filterCriteria"></param>
        /// <param name="sortCriteria"></param>
        /// <returns></returns>
        public ICollection<SmsDomain> SearchSms(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            paging.ValidatePagingCriteria();
            filterCriteria?.ToList().ForEach(x => x.ValidateFilterCriteria());
            sortCriteria?.ToList().ForEach(x => x.ValidateSortCriteria());
            return _smsRepository.SearchSms(paging, filterCriteria, sortCriteria);
        }

        /// <summary>
        /// Update an sms
        /// </summary>
        /// <param name="sms"></param>
        public void UpdateSms(SmsDomain sms)
        {
            ValidateSmsModel(sms);
            ValidationHelper.GreaterThanZero(sms.Id, NotificationMessages.SmsInvalidId);
            ValidationHelper.NotNull(_smsRepository.GetById(sms.Id), NotificationMessages.SmsWithIdDoesNotExists);
            _smsRepository.Update(sms);

        }

        private void ValidateSmsModel(SmsDomain sms)
        {
            ValidationHelper.NotNull(sms, NotificationMessages.SmsNotProvided);
            ValidationHelper.GreaterThanZero(sms.NotificationId, NotificationMessages.SmsInvalidNotificationId);
            ValidationHelper.NotNull(_notificationRepository.GetById(sms.NotificationId), NotificationMessages.SmsNotificationWithIdDoesNotExists);
            ValidationHelper.IsPhoneNumberValid(sms.From, NotificationMessages.SmsInvalidSenderPhoneNumberFormat);
            ValidationHelper.IsPhoneNumberValid(sms.PhoneNumber, NotificationMessages.SmsInvalidPhoneNumberFormat);
        }
    }
}
