using System.Collections.Generic;
using NSI.Domain.Notifications;

namespace NSI.BusinessLogic.Interfaces.Notifications
{
    public interface IEmailRecipientTypeManipulation
    {
        ICollection<EmailRecipientTypeDomain> GetAllEmailRecipientTypes();
        EmailRecipientTypeDomain GetEmailRecipientTypeById(int emailRecipientTypeId);
        EmailRecipientTypeDomain GetEmailRecipientTypeByCode(string emailRecipientTypeCode);    
        int AddEmailRecipientType(EmailRecipientTypeDomain emailRecipientType);
        void UpdateEmailRecipientType(EmailRecipientTypeDomain emailRecipientType);
        void DeleteEmailRecipientType(int emailRecipientTypeId);  
    }
}
