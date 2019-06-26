using NSI.Common.Models;
using NSI.Domain.Notifications;
using System.Collections.Generic;

namespace NSI.BusinessLogic.Interfaces.Notifications
{
    public interface IEmailMessageManipulation
    {
        ICollection<EmailMessageDomain> GetAllEmailMessages();
        EmailMessageDomain GetEmailMessageById(int emailMessageId);
        ICollection<EmailMessageDomain> GetByNotificationId(int notificationId);
        int AddEmailMessage(EmailMessageDomain emailMessage);
        void UpdateEmailMessage(EmailMessageDomain emailMessage);
        ICollection<EmailMessageDomain> SearchEmailMessages(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
        void DeleteEmailMessageById(int emailMessageId);


    }
}
