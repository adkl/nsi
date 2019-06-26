using System.Collections.Generic;
using NSI.Domain.Notifications;

namespace NSI.Repository.Interfaces.Notifications
{
    public interface IAttachmentRepository
    {
        int Add(AttachmentDomain attachment);
        ICollection<AttachmentDomain> GetAll();
        AttachmentDomain GetById(int attachmentId);
        void DeleteById(int attachmentId);
        AttachmentDomain GetByFile(byte[] file);
    }
}
