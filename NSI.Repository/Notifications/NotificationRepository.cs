using NSI.Domain.Notifications;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Extensions.Notifications;
using NSI.Repository.Interfaces.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;


namespace NSI.Repository.Notifications {
    public class NotificationRepository : INotificationRepository {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public NotificationRepository(NsiContext context) {
            _context = context;
        }

        /// <summary>
        /// Add a notification
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public int Add(NotificationDomain notification) {
            var notificationDb = new Notification().AddFromDomainModel(notification);
            _context.Notification.Add(notificationDb);
            _context.SaveChanges();
            return notificationDb.NotificationId;
        }

        /// <summary>
        /// Update a notification
        /// </summary>
        /// <param name="notification"></param>
        public void Update(NotificationDomain notification) {
            var notificationDb = _context.Notification.First(x => x.NotificationId == notification.Id);
            if (notificationDb != null) {
                notificationDb.UpdateFromDomainModel(notification);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Delete notification with id
        /// </summary>
        /// <param name="notificationId"></param>
        public void Delete(int notificationId) {
            var notificationDb = _context.Notification.First(x => x.NotificationId == notificationId);
            if (notificationDb != null) {
                _context.Notification.Remove(notificationDb);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Get all notifications
        /// </summary>
        /// <returns></returns>
        public ICollection<NotificationDomain> GetAll() {
            var result = _context.Notification
                .Include(x => x.NotificationStatus)
                .Include(x => x.NotificationType)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Get notification with id
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        public NotificationDomain GetById(int notificationId) {
            return _context.Notification.FirstOrDefault(x => x.NotificationId == notificationId).ToDomainModel();
        }

        /// <summary>
        /// Get notification by external id
        /// </summary>
        /// <param name="externalId"></param>
        /// <returns></returns>
        public ICollection<NotificationDomain> GetByExternalId(Guid externalId) {
            var result = _context.Notification
                .Where(x => x.ExternalId == externalId)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Get notification by date created
        /// </summary>
        /// <param name="dateCreated"></param>
        /// <returns></returns>
        public ICollection<NotificationDomain> GetByCreatedDate(DateTime dateCreated) {
            var result = _context.Notification
                .Where(x => (
                    x.DateCreated.Year == dateCreated.Year && 
                    x.DateCreated.Month == dateCreated.Month && 
                    x.DateCreated.Day == dateCreated.Day))
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }
    }
}