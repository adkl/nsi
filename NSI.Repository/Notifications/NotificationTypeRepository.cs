using NSI.Domain.Notifications;
using NSI.EF;
using NSI.Repository.Interfaces.Notifications;
using NSI.Repository.Extensions.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Notifications
{
    public class NotificationTypeRepository : INotificationTypeRepository
    {
        private readonly NsiContext _context;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public NotificationTypeRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a notification type
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public int Add(NotificationTypeDomain domain)
        {
            NotificationType notificationType = new NotificationType().FromDomainModel(domain);
            _context.NotificationType.Add(notificationType);
            _context.SaveChanges();
            return notificationType.NotificationTypeId;
        }

        /// <summary>
        /// Get all notification types
        /// </summary>
        /// <returns></returns>
        public ICollection<NotificationTypeDomain> GetAll()
        {
            var result = _context.NotificationType.ToList();
            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Get notification type by id
        /// </summary>
        /// <param name="notificationTypeId"></param>
        /// <returns></returns>
        public NotificationTypeDomain GetById(int notificationTypeId)
        {
            return _context.NotificationType
                    .FirstOrDefault(x => x.NotificationTypeId == notificationTypeId)
                    .ToDomainModel();
        }

        /// <summary>
        /// Get notification type by name
        /// </summary>
        /// <param name="notificationTypeName"></param>
        /// <returns></returns>
        public NotificationTypeDomain GetByName(string notificationTypeName)
        {
            return _context.NotificationType
                    .FirstOrDefault(x => x.Name.Equals(notificationTypeName))
                    .ToDomainModel();
        }

        /// <summary>
        /// Get notification type by code
        /// </summary>
        /// <param name="notificationTypeCode"></param>
        /// <returns></returns>
        public NotificationTypeDomain GetByCode(string notificationTypeCode)
        {
            return _context.NotificationType
                    .FirstOrDefault(x => x.Code.Equals(notificationTypeCode))
                    .ToDomainModel();
        }

        /// <summary>
        /// Get all notification types with code
        /// </summary>
        /// <param name="notificationTypeCode"></param>
        /// <returns></returns>
        public ICollection<NotificationTypeDomain> GetAllByCode(string notificationTypeCode)
        {
            var result = _context.NotificationType.Where(x => x.Code.Equals(notificationTypeCode)).ToList();
            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Update notification type 
        /// </summary>
        /// <param name="domain"></param>
        public void Update(NotificationTypeDomain domain)
        {
            NotificationType notificationType = _context.NotificationType. FirstOrDefault(x => x.NotificationTypeId == domain.Id);
            notificationType.FromDomainModel(domain);
            _context.SaveChanges();
        }
    }
}
