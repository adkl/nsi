using NSI.BusinessLogic.Interfaces.Notifications;
using NSI.Common.Exceptions;
using NSI.Common.Helpers;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.Notifications;
using NSI.Resources.Notifications;
using System.Collections.Generic;

namespace NSI.BusinessLogic.Notifications
{
    public class AttachmentManipulation : IAttachmentManipulation
    {
        private readonly IAttachmentRepository _attachmentRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="attachmentRepository"></param>
        public AttachmentManipulation(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }

        /// <summary>
        /// Adds an attachment
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public int AddAttachment(AttachmentDomain attachment)
        {
            ValidationHelper.NotNull(attachment, NotificationMessages.AttachmentNotProvided);
            ValidationHelper.NotNull(attachment.File, NotificationMessages.AttachmentFileNotProvided);

            return _attachmentRepository.Add(attachment);
        }
        /// <summary>
        /// Delete attachment with id
        /// </summary>
        /// <param name="attachmentId"></param>
        public void DeleteAttachmentById(int attachmentId)
        {

            AttachmentDomain attachmentDomain = _attachmentRepository.GetById(attachmentId);

             if (attachmentDomain == null)
                throw new NsiArgumentException(NotificationMessages.AttachmentWithIdDoesNotExist);

            _attachmentRepository.DeleteById(attachmentId);
        }

        /// <summary>
        /// Get all attachments
        /// </summary>
        /// <returns></returns>
        public ICollection<AttachmentDomain> GetAllAttachments()
        {
            return _attachmentRepository.GetAll();
        }

        /// <summary>
        /// Get attachment by id
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        public AttachmentDomain GetAttachmentById(int attachmentId)
        {
            ValidationHelper.GreaterThanZero(attachmentId, NotificationMessages.AttachmentIdInvalid);

            return _attachmentRepository.GetById(attachmentId);
        }

    }
}
