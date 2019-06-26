using NSI.Common.Models;
using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.Notifications
{

    public interface IEmailRecipientManipulation
    {
        ICollection<EmailRecipientDomain> GetAllEmailRecipients();
        EmailRecipientDomain GetEmailRecipientById(int emailRecipientId);
        int AddEmailRecipient(EmailRecipientDomain emailRecipient);
        void UpdateEmailRecipient(EmailRecipientDomain emailRecipient);
        void DeleteEmailRecipient(int emailRecipientId);
        ICollection<EmailRecipientDomain> SearchEmailRecipients(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);    
    }
}
