using NSI.Domain.Notifications;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Extensions.Notifications;
using NSI.Repository.Interfaces.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace NSI.Repository.Notifications {
    public class NotificationUserRepository : INotificationUserRepository {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public NotificationUserRepository(NsiContext context) {
            _context = context;
        }

        /// <summary>
        /// Add a notification user
        /// </summary>
        /// <param name="notificationUser"></param>
        /// <returns></returns>
        public int Add(NotificationUserDomain notificationUser) {
            var notificationDb = new NotificationUser().FromDomainModel(notificationUser);
            _context.NotificationUser.Add(notificationDb);
            _context.SaveChanges();
            return notificationDb.NotificationUserId;
        }

        /// <summary>
        /// Update notification user
        /// </summary>
        /// <param name="notificationUser"></param>
        public void Update(NotificationUserDomain notificationUser) {
            var notificationDb = _context.NotificationUser.First(x => x.NotificationUserId == notificationUser.Id);
            if (notificationDb != null) {
                notificationDb.FromDomainModel(notificationUser);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Delete notification user with id
        /// </summary>
        /// <param name="notificationUserId"></param>
        public void Delete(int notificationUserId) {
            var notificationDb = _context.NotificationUser.First(x => x.NotificationUserId == notificationUserId);
            if (notificationDb != null) {
                _context.NotificationUser.Remove(notificationDb);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Get all notification users
        /// </summary>
        /// <returns></returns>
        public ICollection<NotificationUserDomain> GetAll() {
            var result = _context.NotificationUser
                .Include(x => x.Notification)
                .Include(x => x.UserInfo)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Get notification user with id
        /// </summary>
        /// <param name="notificationUserId"></param>
        /// <returns></returns>
        public NotificationUserDomain GetById(int notificationUserId) {
            return _context.NotificationUser.FirstOrDefault(x => x.NotificationUserId == notificationUserId).ToDomainModel();
        }

        /// <summary>
        /// Get all notification users with notification id
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        public ICollection<NotificationUserDomain> GetByNotificationId(int notificationId) {
            var result = _context.NotificationUser
                .Where(x => x.NotificationId == notificationId)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Get all notification users with user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ICollection<NotificationUserDomain> GetByUserId(int userId) {
            var result = _context.NotificationUser
                .Where(x => x.UserInfoId == userId)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }
    }
}
