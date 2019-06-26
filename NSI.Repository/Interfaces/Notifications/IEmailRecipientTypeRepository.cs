using System.Collections.Generic;
using NSI.Domain.Notifications;

namespace NSI.Repository.Interfaces.Notifications
{
    public interface IEmailRecipientTypeRepository
    {
        ICollection<EmailRecipientTypeDomain> GetAll();
        EmailRecipientTypeDomain GetById(int emailRecipientTypeId);
        EmailRecipientTypeDomain GetByCode(string emailRecipientTypeCode);
        int Add(EmailRecipientTypeDomain emailRecipientType);
        void Update(EmailRecipientTypeDomain emailRecipientType);
        void Delete(EmailRecipientTypeDomain emailRecipientType);
    }
}
