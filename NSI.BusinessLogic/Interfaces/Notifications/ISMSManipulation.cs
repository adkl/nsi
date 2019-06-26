using NSI.Common.Models;
using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.Notifications
{
    public interface ISmsManipulation
    {
        int AddSms(SmsDomain sms);
        void UpdateSms(SmsDomain sms);
        void DeleteSms(int Id);
        ICollection<SmsDomain> GetByNotificationId(int notificationId);
        ICollection<SmsDomain> SearchSms(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
        SmsDomain GetSmsById(int id);
        SmsDomain GetSmsByPhoneNumber(String number);
        SmsDomain GetSmsByFrom(String from);
    }
}
