using NSI.Domain.Notifications;
using NSI.EF;
using NSI.Repository.Extensions.Notifications;
using NSI.Repository.Interfaces.Notifications;
using NSI.Repository.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace NSI.Repository.Notifications
{
    public class AttachmentRepository : IAttachmentRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public AttachmentRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a new attachment
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public int Add(AttachmentDomain attachment)
        {
            var attachmentDb = new Attachment().FromDomainModel(attachment);
            _context.Attachment.Add(attachmentDb);
            _context.SaveChanges();
            return attachmentDb.AttachmentId;
        }

        /// <summary>
        /// Delete attachment with Id
        /// </summary>
        /// <param name="attachmentId"></param>
        public void DeleteById(int attachmentId)
        {
            var attachmentDb = _context.Attachment.First(x => x.AttachmentId == attachmentId);
            if (attachmentDb != null)
            {
                _context.Attachment.Remove(attachmentDb);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Get all atttachments
        /// </summary>
        /// <returns></returns>
        public ICollection<AttachmentDomain> GetAll()
        {
            var result = _context.Attachment
               .Include(x => x.Notification)
               .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Get attachment by file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public AttachmentDomain GetByFile(byte[] file)
        {
            return _context.Attachment.FirstOrDefault(x => x.File.Equals(file)).ToDomainModel();
        }

        /// <summary>
        /// Get attachment by id
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        public AttachmentDomain GetById(int attachmentId)
        {
            return _context.Attachment.FirstOrDefault(x => x.AttachmentId == attachmentId).ToDomainModel();
        }
    }
}
