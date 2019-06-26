using NSI.Domain.Notifications;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions.Notifications
{
    public static class SmsExtension
    {
        public static SmsDomain ToDomainModel(this Sms obj)
        {
            return obj == null ? null : new SmsDomain()
            {
                Id = obj.SmsId,
                PhoneNumber = obj.PhoneNumber,
                From = obj.From,
                NotificationId = obj.NotificationId,
                //Notification = obj.Notification.ToDomainModel()
            };
        }

        public static Sms FromDomainModel(this Sms obj, SmsDomain smsDomain)
        {
            if (obj == null)
            {
                obj = new Sms();
            }

            obj.SmsId = smsDomain.Id;
            obj.PhoneNumber = smsDomain.PhoneNumber;
            obj.From = smsDomain.From;
            obj.NotificationId = smsDomain.NotificationId;
            return obj;
        }
    }
}
