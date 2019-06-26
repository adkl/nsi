using NSI.Domain.Notifications;
using NSI.EF;

namespace NSI.Repository.Extensions.Notifications
{
    public static class EmailRecipientTypeExtension
    {
        public static EmailRecipientTypeDomain ToDomainModel(this EmailRecipientType obj)
        {
            return obj == null ? null : new EmailRecipientTypeDomain()
            {
                Id = obj.EmailRecipientTypeId,
                Name = obj.Name,
                Code = obj.Code
            };
        }

        public static EmailRecipientType FromDomainModel(this EmailRecipientType obj, EmailRecipientTypeDomain domain)
        {
            if (obj == null)
            {
                obj = new EmailRecipientType();
            }

            obj.EmailRecipientTypeId = domain.Id;
            obj.Name = domain.Name;
            obj.Code = domain.Code;
            return obj;
        }

        public static EmailRecipientType UpdateFromDomainModel(this EmailRecipientType obj, EmailRecipientTypeDomain domain)
        {
            obj.Name = domain.Name;
            return obj;
        }
    }
}
