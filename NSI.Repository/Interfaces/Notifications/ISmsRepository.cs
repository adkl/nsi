using NSI.Common.Models;
using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.Notifications
{
    public interface ISmsRepository
    {
        int Add(SmsDomain sms);
        void Update(SmsDomain sms);
        void Delete(SmsDomain sms);
        ICollection<SmsDomain> GetByNotificationId(int Id);
        SmsDomain GetById(int id);
        SmsDomain GetByPhoneNumber(String phoneNumber);
        SmsDomain GetByFrom(String from);
        ICollection<SmsDomain> SearchSms(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);

    }
}
