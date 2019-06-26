using NSI.Common.Models;
using NSI.Domain.Notifications;
using NSI.EF;
using NSI.Repository.Extensions.Notifications;
using NSI.Repository.Interfaces.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using NSI.Common.Extensions;
using System.Linq.Expressions;
using NSI.Repository.Extensions;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Notifications
{
    public class EmailRecipientRepository : IEmailRecipientRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public EmailRecipientRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a single email recipient to db
        /// </summary>
        /// <param name="emailRecipient"><see cref="EmailRecipientDomain"/></param>
        /// <returns></returns>
        public int Add(EmailRecipientDomain emailRecipient)
        {
            var emailRecipientDb = new EmailRecipient().FromDomainModel(emailRecipient);
            _context.EmailRecipient.Add(emailRecipientDb);
            _context.SaveChanges();
            return emailRecipientDb.EmailRecipientId;
        }

        /// <summary>
        /// Get all email recipients
        /// </summary>
        /// <returns></returns>
        public ICollection<EmailRecipientDomain> GetAll()
        {
            var result = _context.EmailRecipient
                .Include(x => x.EmailMessage)
                .Include(x => x.EmailRecipientType)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Get email recipient by id
        /// </summary>
        /// <param name="emailRecipientId"></param>
        /// <returns></returns>
        public EmailRecipientDomain GetById(int emailRecipientId)
        {
            return _context.EmailRecipient.FirstOrDefault(x => x.EmailRecipientId == emailRecipientId).ToDomainModel();
        }

        /// <summary>
        /// Get email recipient by email address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public EmailRecipientDomain GetByEmailAddress(string emailAddress)
        {
            return _context.EmailRecipient.FirstOrDefault(x => x.EmailAddress == emailAddress).ToDomainModel();
        }

        /// <summary>
        /// Update an existing email recipient
        /// </summary>
        /// <param name="emailRecipient"></param>
        public void Update(EmailRecipientDomain emailRecipient)
        {
            var emailRecipientDb = _context.EmailRecipient.FirstOrDefault(x => x.EmailRecipientId == emailRecipient.Id);
            emailRecipientDb.UpdateFromDomainModel(emailRecipient);
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete email recipient with id
        /// </summary>
        /// <param name="emailrecipientId"></param>
        public void Delete(int emailrecipientId)
        {
            var emailRecipientDb = _context.EmailRecipient.First(x => x.EmailRecipientId == emailrecipientId);
            if (emailRecipientDb != null)
            {
                _context.EmailRecipient.Remove(emailRecipientDb);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Search email recipients by search and filter criteria
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="filterCriteria"></param>
        /// <param name="sortCriteria"></param>
        /// <returns></returns>
        public ICollection<EmailRecipientDomain> SearchEmailRecipients(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            var result = _context.EmailRecipient
                .Include(x => x.EmailMessage)
                .Include(x => x.EmailRecipientType)
                .DoFiltering(filterCriteria, FilterEmailRecipients)
                .DoSorting(sortCriteria, SortEmailRecipients)
                .DoPaging(paging)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        private Expression<Func<EmailRecipient, object>> SortEmailRecipients(string columnName)
        {
            Expression<Func<EmailRecipient, object>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "emailaddress":
                    fnc = x => x.EmailAddress;
                    break;
                default:
                    break;
            }

            return fnc;
        }

        private Expression<Func<EmailRecipient, bool>> FilterEmailRecipients(string columnName, string filterTerm, bool isExactMatch)
        {
            Expression<Func<EmailRecipient, bool>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "emailaddress":
                    if (!isExactMatch)
                    {
                        fnc = x => (x.EmailAddress).Contains(filterTerm);
                    }
                    else
                    {
                        fnc = x => x.EmailAddress == filterTerm;
                    }
                    break;
            }

            return fnc;
        }

    }
}
