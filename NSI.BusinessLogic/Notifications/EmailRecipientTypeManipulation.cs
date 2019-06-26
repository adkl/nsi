using System.Collections.Generic;
using NSI.BusinessLogic.Interfaces.Notifications;
using NSI.Repository.Interfaces.Notifications;
using NSI.Domain.Notifications;
using NSI.Common.Helpers;
using NSI.Resources.Membership;
using NSI.Common.Models;
using System.Linq;
using NSI.Common.Exceptions;
using NSI.Common.Extensions;
using NSI.Resources.Notifications;

namespace NSI.BusinessLogic.Notifications
{
    public class EmailRecipientTypeManipulation : IEmailRecipientTypeManipulation
    {
        private readonly IEmailRecipientTypeRepository _emailRecipientTypeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="emailRecipientTypeRepository"></param>
        public EmailRecipientTypeManipulation(IEmailRecipientTypeRepository emailRecipientTypeRepository)
        {
            _emailRecipientTypeRepository = emailRecipientTypeRepository;
        }

        /// <summary>
        /// Adda single email recipient type
        /// </summary>
        /// <param name="emailRecipientType"></param>
        /// <returns></returns>
        public int AddEmailRecipientType(EmailRecipientTypeDomain emailRecipientType)
        {
            ValidateEmailRecipientTypeModel(emailRecipientType);

            var emailRecipientTypeWithProvidedcode = _emailRecipientTypeRepository.GetByCode(emailRecipientType.Code.SafeTrim());

            if(emailRecipientTypeWithProvidedcode != null)
            {
                throw new NsiArgumentException(NotificationMessages.EmailRecipientTypeCodeAlreadyExists, Common.Enumerations.SeverityEnum.Warning);
            }

            return _emailRecipientTypeRepository.Add(emailRecipientType);
        }

        /// <summary>
        /// Delete email recipient type with id
        /// </summary>
        /// <param name="emailRecipientTypeId"></param>
        public void DeleteEmailRecipientType(int emailRecipientTypeId)
        {
            EmailRecipientTypeDomain emailRecipientType = _emailRecipientTypeRepository.GetById(emailRecipientTypeId);
            if (emailRecipientType == null)
                throw new NsiArgumentException(NotificationMessages.EmailRecipientTypeWithIdDoesNotExist);

            ValidationHelper.GreaterThanZero(emailRecipientTypeId, NotificationMessages.EmailRecipientTypeIdInvalid);
            ValidationHelper.NotNull(emailRecipientType, NotificationMessages.EmailRecipientTypeWithIdDoesNotExist);
            ValidateEmailRecipientTypeModel(emailRecipientType);

            _emailRecipientTypeRepository.Delete(emailRecipientType);
        }

        /// <summary>
        /// Get all email recipient types
        /// </summary>
        /// <returns></returns>
        public ICollection<EmailRecipientTypeDomain> GetAllEmailRecipientTypes()
        {
            return _emailRecipientTypeRepository.GetAll();
        }

        /// <summary>
        /// Get email recipient type by code
        /// </summary>
        /// <param name="emailRecipientTypeCode"></param>
        /// <returns></returns>
        public EmailRecipientTypeDomain GetEmailRecipientTypeByCode(string emailRecipientTypeCode)
        {
            ValidationHelper.NotNullOrWhitespace(emailRecipientTypeCode, NotificationMessages.EmailRecipientTypeCodeEmpty);
            ValidationHelper.MaxLength(emailRecipientTypeCode, 100, NotificationMessages.EmailRecipientTypeCodeLengthExceeded);
            return _emailRecipientTypeRepository.GetByCode(emailRecipientTypeCode);
            
        }

        /// <summary>
        /// Get email recipient type by id
        /// </summary>
        /// <param name="emailRecipientTypeId"></param>
        /// <returns></returns>
        public EmailRecipientTypeDomain GetEmailRecipientTypeById(int emailRecipientTypeId)
        {
            ValidationHelper.GreaterThanZero(emailRecipientTypeId, NotificationMessages.EmailRecipientTypeIdInvalid);

            return _emailRecipientTypeRepository.GetById(emailRecipientTypeId);
        }

        /// <summary>
        /// Update an email recipient type
        /// </summary>
        /// <param name="emailRecipientType"></param>
        public void UpdateEmailRecipientType(EmailRecipientTypeDomain emailRecipientType)
        {            
            if(emailRecipientType == null)
            {
                throw new NsiArgumentException(NotificationMessages.EmailRecipientTypeWithIdDoesNotExist);
            }
            ValidationHelper.GreaterThanZero(emailRecipientType.Id, NotificationMessages.EmailRecipientTypeIdInvalid);
            ValidationHelper.NotNull(_emailRecipientTypeRepository.GetById(emailRecipientType.Id), NotificationMessages.EmailRecipientTypeWithIdDoesNotExist);
            _emailRecipientTypeRepository.Update(emailRecipientType);
        }

        private void ValidateEmailRecipientTypeModel(EmailRecipientTypeDomain emailRecipientType)
        {
            ValidationHelper.NotNull(emailRecipientType, NotificationMessages.EmailRecipientTypeNotProvided);
            ValidationHelper.NotNullOrWhitespace(emailRecipientType.Code, NotificationMessages.EmailRecipientTypeCodeEmpty);
            ValidationHelper.MaxLength(emailRecipientType.Code, 100, NotificationMessages.EmailRecipientTypeCodeLengthExceeded);
            ValidationHelper.MaxLength(emailRecipientType.Name, 100, NotificationMessages.EmailRecipientTypeNameLengthExceeded);
        }
    }
}
