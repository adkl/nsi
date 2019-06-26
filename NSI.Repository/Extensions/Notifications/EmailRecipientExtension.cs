using NSI.Domain.Notifications;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions.Notifications
{
    public static class EmailRecipientExtension
    {
        public static EmailRecipientDomain ToDomainModel(this EmailRecipient obj)
        {
            return obj == null ? null : new EmailRecipientDomain()
            {
                Id = obj.EmailRecipientId,
                EmailAddress = obj.EmailAddress,
                EmailRecipientTypeId = obj.EmailRecipientTypeId,
            };
        }

        public static EmailRecipient FromDomainModel(this EmailRecipient obj, EmailRecipientDomain domain)
        {
            if (obj == null)
            {
                obj = new EmailRecipient();
            }

            obj.EmailRecipientId = domain.Id;
            obj.EmailAddress = domain.EmailAddress;
            obj.EmailRecipientTypeId = domain.EmailRecipientTypeId;
            obj.EmaiMessagelId = domain.EmaiMessagelId;

            return obj;
        }

        public static EmailRecipient UpdateFromDomainModel(this EmailRecipient obj, EmailRecipientDomain domain)
        {
            obj.EmailAddress = domain.EmailAddress;
            return obj;
        }


    }
}
