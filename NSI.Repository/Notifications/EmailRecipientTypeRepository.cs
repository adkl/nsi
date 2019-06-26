using NSI.Domain.Notifications;
using NSI.EF;
using NSI.Repository.Extensions.Notifications;
using NSI.Repository.Interfaces.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace NSI.Repository.Notifications
{
    public class EmailRecipientTypeRepository : IEmailRecipientTypeRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public EmailRecipientTypeRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new email recipient type to the database
        /// </summary>
        /// <param name="emailRecipientType">Email recipient type information to be added. Instance of <see cref="EmailRecipientTypeDomain"/></param>
        /// <returns>EmailRecipientTypeId of the created email recipient type</returns>
        public int Add(EmailRecipientTypeDomain emailRecipientType)
        {
            var emailRecipientTypeDb = new EmailRecipientType().FromDomainModel(emailRecipientType);
            _context.EmailRecipientType.Add(emailRecipientTypeDb);
            _context.SaveChanges();
            return emailRecipientTypeDb.EmailRecipientTypeId;
        }

        /// <summary>
        /// Deletes a single email recipient type with provided ID
        /// </summary>
        /// <param name="emailRecipientType"></param>
        public void Delete(EmailRecipientTypeDomain emailRecipientType)
        {
            var emailRecipientTypeDb = _context.EmailRecipientType.First(x => x.EmailRecipientTypeId == emailRecipientType.Id);
            if (emailRecipientTypeDb != null)
            {
                emailRecipientTypeDb.FromDomainModel(emailRecipientType);
                _context.EmailRecipientType.Remove(emailRecipientTypeDb);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all email recipient types from the database
        /// </summary>
        /// <returns><see cref="ICollection{EmailRecipientTypeDomain}"/></returns>
        public ICollection<EmailRecipientTypeDomain> GetAll()
        {
            var result = _context.EmailRecipientType
            .Include(x => x.EmailRecipient)   
            .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();

        }

        /// <summary>
        /// Get email recipient type by code
        /// </summary>
        /// <param name="emailRecipientTypeCode"></param>
        /// <returns></returns>
        public EmailRecipientTypeDomain GetByCode(string emailRecipientTypeCode)
        {
            return _context.EmailRecipientType
                .Include(x => x.EmailRecipient)
                .FirstOrDefault(x => x.Code == emailRecipientTypeCode).ToDomainModel();
        }

        /// <summary>
        /// Retrieves a single email recipient type with provided ID
        /// </summary>
        /// <param name="emailRecipientTypeId"></param>
        /// <returns>Email recipient type if it exists, instance of <see cref="EmailRecipientTypeDomain"/></returns>
        public EmailRecipientTypeDomain GetById(int emailRecipientTypeId)
        {
            return _context.EmailRecipientType
                .Include(x => x.EmailRecipient)
                .FirstOrDefault(x => x.EmailRecipientTypeId == emailRecipientTypeId).ToDomainModel();
        }

        /// <summary>
        /// Updates an email recipient type with data from the provided email recipient type
        /// </summary>
        /// <param name="emailRecipientType"></param>
        public void Update(EmailRecipientTypeDomain emailRecipientType)
        {
            var emailRecipientTypeDb = _context.EmailRecipientType.FirstOrDefault(x => x.EmailRecipientTypeId == emailRecipientType.Id);
            emailRecipientTypeDb.UpdateFromDomainModel(emailRecipientType);
            _context.SaveChanges();
        }
    }
}
