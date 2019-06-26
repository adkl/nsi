using NSI.Common.Models;
using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.Notifications
{
    public interface IEmailRecipientRepository
    {
        ICollection<EmailRecipientDomain> GetAll();
        EmailRecipientDomain GetById(int emailRecipientId);
        EmailRecipientDomain GetByEmailAddress(string emailAddress);
        int Add(EmailRecipientDomain emailRecipient);
        void Update(EmailRecipientDomain emailRecipient);
        void Delete(int emailRecipientId);
        ICollection<EmailRecipientDomain> SearchEmailRecipients(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
    }
}
