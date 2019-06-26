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
    public class NotificationStatusRepository : INotificationStatusRepository
    {
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public NotificationStatusRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a single notification status
        /// </summary>
        /// <param name="notificationStatus"></param>
        /// <returns></returns>
        public int AddNotificationStatus(NotificationStatusDomain notificationStatus)
        {
            var notificationStatusDb = new NotificationStatus().FromDomainModel(notificationStatus);
            _context.NotificationStatus.Add(notificationStatusDb);
            _context.SaveChanges();
            return notificationStatusDb.NotificationStatusId;
        }

        /// <summary>
        /// Update notification status
        /// </summary>
        /// <param name="notificationStatus"></param>
        public void UpdateNotificationStatus(NotificationStatusDomain notificationStatus)
        {
            var notificationStatusDb = _context.NotificationStatus.FirstOrDefault(x => x.NotificationStatusId == notificationStatus.Id);
            notificationStatusDb.FromDomainModel(notificationStatus);
            _context.SaveChanges();
        }

        /// <summary>
        /// Get all notification statuses
        /// </summary>
        /// <returns></returns>
        public ICollection<NotificationStatusDomain> GetAll()
        {
            var result = _context.NotificationStatus
                .Include(x => x.Notification)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Get notification status by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public NotificationStatusDomain GetByCode(string code)
        {
            return _context.NotificationStatus
                .FirstOrDefault(x => x.Code == code).ToDomainModel();
        }

        /// <summary>
        /// Get notification status by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NotificationStatusDomain GetById(int id)
        {
            return _context.NotificationStatus
                .FirstOrDefault(x => x.NotificationStatusId == id).ToDomainModel();
        }

        /// <summary>
        /// Get notification status by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public NotificationStatusDomain GetByName(string name)
        {
            return _context.NotificationStatus
                .FirstOrDefault(x => x.Name == name).ToDomainModel();
        }

        /// <summary>
        /// Search notification statuses by search and filter criteria
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="filterCriteria"></param>
        /// <param name="sortCriteria"></param>
        /// <returns></returns>
        public ICollection<NotificationStatusDomain> SearchNotificationStatus(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            var result = _context.NotificationStatus
                .DoFiltering(filterCriteria, FilterNotificationStatuses)
                .DoSorting(sortCriteria, SortNotificationStatuses)
                .DoPaging(paging)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        private Expression<Func<NotificationStatus, object>> SortNotificationStatuses(string columnName)
        {
            Expression<Func<NotificationStatus, object>> fnc = null;

            switch(columnName.ToLowerInvariant())
            {
                case "name":
                    fnc = x => x.Name;
                    break;
                case "code":
                    fnc = x => x.Code;
                    break;
                default:
                    break;
            }

            return fnc;
        }

        private Expression<Func<NotificationStatus, bool>> FilterNotificationStatuses(string columnName, string filterTerm, bool isExactMatch)
        {
            Expression<Func<NotificationStatus, bool>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "name":
                    if(!isExactMatch)
                    {
                        fnc = x => (x.Name).Contains(filterTerm);
                    } else
                    {
                        fnc = x => x.Name == filterTerm;
                    }
                    break;
                case "code":
                    if (!isExactMatch)
                    {
                        fnc = x => (x.Code).Contains(filterTerm);
                    }
                    else
                    {
                        fnc = x => x.Code == filterTerm;
                    }
                    break;
                default:
                    break;
            }

            return fnc;
        }

        /// <summary>
        /// Delete notification status with id
        /// </summary>
        /// <param name="notificationStatus"></param>
        public void DeleteNotificationStatus(NotificationStatusDomain notificationStatus)
        {
            var notificationStatusDb = _context.NotificationStatus.First(x => x.NotificationStatusId == notificationStatus.Id);
            if (notificationStatusDb != null)
            {
                notificationStatusDb.FromDomainModel(notificationStatus);
                _context.NotificationStatus.Remove(notificationStatusDb);
                _context.SaveChanges();
            }
        }
    }
}
