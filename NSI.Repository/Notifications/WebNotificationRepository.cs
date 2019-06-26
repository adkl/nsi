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
    public class WebNotificationRepository: IWebNotificationRepository
    {
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public WebNotificationRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a web notification
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public int Add(WebNotificationDomain domain)
        {
            WebNotification webNotification = new WebNotification().FromDomainModel(domain);
            _context.WebNotification.Add(webNotification);
            _context.SaveChanges();
            return webNotification.WebNotificationId;
        }

        /// <summary>
        /// Get all  web notifications
        /// </summary>
        /// <returns></returns>
        public ICollection<WebNotificationDomain> GetAll()
        {
            var result = _context.WebNotification.ToList();
            return result.Select(x => x.ToDomainModel(_context.Notification.FirstOrDefault(y => y.NotificationId == x.NotificationId).ToDomainModel())).ToList();
        }

        /// <summary>
        /// Get web notification by id
        /// </summary>
        /// <param name="webNotificationId"></param>
        /// <returns></returns>
        public WebNotificationDomain GetById(int webNotificationId)
        {
            WebNotification webNotification = _context.WebNotification.FirstOrDefault(x => x.WebNotificationId == webNotificationId);
            NotificationDomain notificationDomain = _context.Notification.FirstOrDefault(x => x.NotificationId == webNotification.NotificationId).ToDomainModel();

            return webNotification.ToDomainModel(notificationDomain);
        }

        /// <summary>
        /// Update a web notification
        /// </summary>
        /// <param name="domain"></param>
        public void Update(WebNotificationDomain domain)
        {
            WebNotification webNotification = _context.WebNotification.FirstOrDefault(x => x.WebNotificationId == domain.Id);
            webNotification.FromDomainModel(domain);
            _context.SaveChanges();
        }

        /// <summary>
        /// Update multiple web notifications
        /// </summary>
        /// <param name="domains"></param>
        public void Update(ICollection<WebNotificationDomain> domains)
        {
            foreach(WebNotificationDomain domain in domains)
            {
                WebNotification webNotification = _context.WebNotification.FirstOrDefault(x => x.WebNotificationId == domain.Id);
                webNotification.FromDomainModel(domain);
            }
            _context.SaveChanges();
        }
    }
}
