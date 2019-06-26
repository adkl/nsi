using NSI.Domain.Notifications;
using NSI.EF;
using System;

namespace NSI.Repository.Extensions.Notifications
{
    public static class AttachmentExtension
    {
        public static AttachmentDomain ToDomainModel(this Attachment obj)
        {
            return obj == null ? null : new AttachmentDomain()
            {
                Id = obj.AttachmentId,
                File = obj.File,
                DateCreated = obj.DateCreated,

            };
        }

        public static Attachment FromDomainModel(this Attachment obj, AttachmentDomain domain)
        {
            if (obj == null)
            {
                obj = new Attachment();
            }

            obj.AttachmentId = domain.Id;
            obj.File = domain.File;
            obj.DateCreated = DateTime.Now;

            return obj;
        }
    }
}
