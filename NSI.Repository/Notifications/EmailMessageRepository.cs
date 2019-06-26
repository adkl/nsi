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
using System.Text.RegularExpressions;

namespace NSI.Repository.Notifications
{
    public class EmailMessageRepository : IEmailMessageRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public EmailMessageRepository(NsiContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Add a new email message
        /// </summary>
        /// <param name="emailMessage"><see cref="EmailMessageDomain"/></param>
        /// <returns></returns>
        public int Add(EmailMessageDomain emailMessage)
        {
            var emailMessageDb = new EmailMessage().FromDomainModel(emailMessage);
            _context.EmailMessage.Add(emailMessageDb);
            _context.SaveChanges();
            return emailMessageDb.EmaiMessagelId;
        }

        /// <summary>
        /// Get all email messages
        /// </summary>
        /// <returns></returns>
        public ICollection<EmailMessageDomain> GetAll()
        {
            var result = _context.EmailMessage
                .Include(x => x.EmailRecipient)
                .Include(x => x.Notification)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Get email message by id
        /// </summary>
        /// <param name="emaiMessageId"></param>
        /// <returns></returns>
        public EmailMessageDomain GetById(int emaiMessageId)
        {
            return _context.EmailMessage.FirstOrDefault(x => x.EmaiMessagelId == emaiMessageId).ToDomainModel();
        }

        /// <summary>
        /// Get all email messages with notification id
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        public ICollection<EmailMessageDomain> GetByNotificationId(int notificationId)
        {
            var result = _context.EmailMessage
               .Where(x => x.NotificationId == notificationId)
               .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Get an email message by providing a from email
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public EmailMessageDomain GetByFrom(string from)
        {
            return _context.EmailMessage.FirstOrDefault(x => x.From.Equals(from)).ToDomainModel();
        }

        /// <summary>
        /// Update an existing email message
        /// </summary>
        /// <param name="emailMessage"></param>
        public void Update(EmailMessageDomain emailMessage)
        {
            var emailMessageDb = _context.EmailMessage.FirstOrDefault(x => x.EmaiMessagelId == emailMessage.Id);
            emailMessageDb.FromDomainModel(emailMessage);
            _context.SaveChanges();
        }

        /// <summary>
        /// Search email message by search and filter criteria
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="filterCriteria"></param>
        /// <param name="sortCriteria"></param>
        /// <returns></returns>
        public ICollection<EmailMessageDomain> SearchEmailMessages(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            var result = _context.EmailMessage
                .Include(x => x.EmailRecipient)
                .Include(x => x.Notification)
                .DoFiltering(filterCriteria, FilterEmailMessages)
                .DoSorting(sortCriteria, SortEmailMessages)
                .DoPaging(paging)
                .ToList();
            return result.Select(x => x.ToDomainModel()).ToList();
        }
        private Expression<Func<EmailMessage, bool>> FilterEmailMessages(string columnName, string filterTerm, bool isExactMatch)
        {
            Expression<Func<EmailMessage, bool>> fnc = null;
            switch (columnName.ToLowerInvariant())
            {
                case "from":
                    if (!isExactMatch)
                    {
                        fnc = x => (x.From).Contains(filterTerm);
                    }
                    else
                    {
                        fnc = x => x.From == filterTerm;
                    }
                    break;
                case "notification":
                case "notificationId":
                    if (Regex.IsMatch(filterTerm, @"^\d+$"))
                    {
                        if (!isExactMatch)
                        {
                            fnc = x => (x.NotificationId.ToString()).Contains(filterTerm);
                        }
                        else
                        {
                            fnc = x => x.NotificationId.ToString() == filterTerm;
                        }
                    }
                    break;
                default:
                    break;
            }
            return fnc;
        }
        private Expression<Func<EmailMessage, object>> SortEmailMessages(string columnName)
        {
            Expression<Func<EmailMessage, object>> fnc = null;
            switch (columnName.ToLowerInvariant())
            {
                case "from":
                    fnc = x => x.From;
                    break;
                case "notification":
                case "notificationId":
                    fnc = x => x.NotificationId.ToString();
                    break;
                default:
                    break;
            }
            return fnc;
        }

        /// <summary>
        /// Delete an email message with id
        /// </summary>
        /// <param name="emailMessageId"></param>
        public void DeleteById(int emailMessageId)
        {
            var emailMessageDb = _context.EmailMessage.First(x => x.EmaiMessagelId == emailMessageId);
            if (emailMessageDb != null)
            {
                _context.EmailMessage.Remove(emailMessageDb);
                _context.SaveChanges();
            }
        }
    }
}
