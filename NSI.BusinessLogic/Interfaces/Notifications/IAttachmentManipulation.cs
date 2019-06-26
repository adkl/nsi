using NSI.Common.Models;
using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;

namespace NSI.BusinessLogic.Interfaces.Notifications
{
    public interface IAttachmentManipulation
    {
        ICollection<AttachmentDomain> GetAllAttachments();
        AttachmentDomain GetAttachmentById(int attachmentId);
        int AddAttachment(AttachmentDomain attachment);
        void DeleteAttachmentById(int attachmentId);
    }
}
