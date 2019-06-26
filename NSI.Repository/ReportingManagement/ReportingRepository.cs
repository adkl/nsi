using NSI.Common.Exceptions;
using NSI.Domain.ReportingManagement;
using NSI.EF;
using NSI.Repository.Interfaces.ReportingManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.ReportingManagement
{
    public class ReportingRepository : IReportingRepository
    {
        private readonly NsiContext _context = new NsiContext();
        public UsersActivityWrapper GetActiveUsers(int tenantId)
        {
            if (tenantId <= 0) throw new NsiArgumentException("Pogrešan id tenanta.");
            UsersActivityWrapper usersActivityWrapper = new UsersActivityWrapper();

            usersActivityWrapper.ActiveUsers = _context.UserInfo.Where(x => x.TenantId == tenantId && x.IsActive).Count();
            usersActivityWrapper.InactiveUsers = _context.UserInfo.Where(x => x.TenantId == tenantId && !x.IsActive).Count();
            
            var result = _context.UserInfo.Where(x => x.TenantId == tenantId).ToList();
            List<UserData> users = new List<UserData>();
            foreach(UserInfo u in result)
            {
                UserData userData = new UserData();
                userData.Name = u.FirstName;
                userData.Surname = u.LastName;
                userData.Status = u.IsActive;
                userData.Email = u.Email;
                users.Add(userData);
            }
            usersActivityWrapper.Users = users;
            return usersActivityWrapper;
        }

        public List<FrequentIncidentsWrapper> GetFrequentIncidents(int tenantId, DateTime dateFrom, DateTime dateTo)
        {
            var q = (from i in _context.Incident
                      where i.DateCreated >= dateFrom && i.DateCreated <= dateTo && tenantId == i.TenantId
                      group i by i.IncidentTypeId into incident
                      join it in _context.IncidentType on incident.Key equals it.IncidentTypeId
                      select new FrequentIncidentsWrapper
                      {
                          IncidentTypeId = incident.Key,
                          IncidentTypeName = it.Name,
                          TimeFrom = dateFrom,
                          TimeTo = dateTo,
                          NumberOfIncidents = incident.Count()
                      }
                      ).ToList();
            q.Sort((x, y) => y.NumberOfIncidents.CompareTo(x.NumberOfIncidents));

            return q;
        }

        public List<FrequentDevicesWrapper> GetFrequentDevices()
        {
            var q = (from d in _context.Device
                     join dt in _context.DeviceType on d.DeviceTypeId equals dt.DeviceTypeId
                     where dt.IsActive == true
                     group dt by dt.Name into device
                     select new FrequentDevicesWrapper
                     {
                         DeviceTypeName = device.Key,
                         DevicesCount = device.Count()
                     }
                     ).ToList();
            q.Sort((x, y) => y.DevicesCount.CompareTo(x.DevicesCount));

            return q;
        }

        public List<LoggingDataWrapper> GetUserLoggingData(DateTime dateFrom, DateTime dateTo)
        {
            List<Log> temp = _context.Log.ToList();
            List<Log> logs = new List<Log>();
            foreach(Log l in temp)
            {
                DateTime logged = DateTime.Parse(l.Logged);
                if (logged >= dateFrom && logged <= dateTo)
                    logs.Add(l);
            }

            List<LoggingDataWrapper> data = new List<LoggingDataWrapper>();
            foreach (Log l in logs)
            {
                LoggingDataWrapper t = new LoggingDataWrapper();
                t.MachineName = l.MachineName;
                t.LogsCount = logs.Where(x => x.MachineName == t.MachineName).Count();
                data.Add(t);
            }

            List<LoggingDataWrapper> result = data.GroupBy(x => x.MachineName).Select(y => y.First()).ToList();
            return result;
        }

        public SmsDataWrapper GetSmsSendingData(DateTime dateFrom, DateTime dateTo)
        {
            var q = (from s in _context.Sms
                    join n in _context.Notification on s.NotificationId equals n.NotificationId
                    where n.DateCreated >= dateFrom && n.DateCreated <= dateTo
                    select new Domain.ReportingManagement.Sms {
                        From = s.From,
                        PhoneNumber = s.PhoneNumber,
                        Content = s.Notification.Content
                    }).ToList();
            SmsDataWrapper result = new SmsDataWrapper();
            result.SmsCount = q.Count();
            result.Sms = q;
            return result;
        }
    }
}
