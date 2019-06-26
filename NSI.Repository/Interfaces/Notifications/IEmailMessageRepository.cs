using NSI.Common.Models;
using NSI.Domain.Notifications;
using System.Collections.Generic;

namespace NSI.Repository.Interfaces.Notifications
{
    public interface IEmailMessageRepository
    {
        int Add(EmailMessageDomain emailMessage);
        ICollection<EmailMessageDomain> GetAll();
        EmailMessageDomain GetById(int emailMessageId);
        ICollection<EmailMessageDomain> GetByNotificationId(int notificationId);
        void Update(EmailMessageDomain emailMessage);
        ICollection<EmailMessageDomain> SearchEmailMessages(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
        void DeleteById(int emailMessageId);
        EmailMessageDomain GetByFrom(string from);
        
    }
}
