using NSI.Domain.Notifications;
using NSI.EF;

namespace NSI.Repository.Extensions.Notifications
{
    public static class EmailMessageExtension
    {

        public static EmailMessageDomain ToDomainModel(this EmailMessage obj)
        {
            return obj == null ? null : new EmailMessageDomain()
            {
                Id = obj.EmaiMessagelId,
                From = obj.From,
                NotificationId = obj.NotificationId,
     
            };
        }
        public static EmailMessage FromDomainModel(this EmailMessage obj, EmailMessageDomain domain)
        {
            if (obj == null)
            {
                obj = new EmailMessage();
            }

            obj.EmaiMessagelId = domain.Id;
            obj.From = domain.From;
            obj.NotificationId = domain.NotificationId;           

            return obj;
        }
    }
}
