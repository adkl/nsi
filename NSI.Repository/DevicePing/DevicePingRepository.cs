using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSI.Domain.DevicePing;
using NSI.EF;
using NSI.Repository.Interfaces.DevicePing;
using NSI.Repository.Extensions;
using System.Data.Entity;
using NSI.Common.Models;
using NSI.Common.Extensions;
using System.Linq.Expressions;
using NSI.Domain.Membership;
using NSI.Common.Exceptions;

namespace NSI.Repository.DevicePing
{
    public class DevicePingRepository : IDevicePingRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public DevicePingRepository(NsiContext context)
        {
            _context = context;
        }

        public int AddDevicePing(int tenantId, DevicePingDomain device)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device cannot be null");
            }

            if (tenantId <= 0) throw new NsiArgumentException("Invalid tenant ID");

            device.TenantId = tenantId;

            var devicePingDb = new NSI.EF.DevicePing().FromDomainModel(device);
             _context.DevicePing.Add(devicePingDb);

            // Set devicePingId to newly added record id
            device.DevicePropertyValues.ForEach(dpv => {
                dpv.DevicePingId = devicePingDb.DevicePingId;
                dpv.TenantId = tenantId;
            });

            device.DevicePropertyValues.ForEach(devicePropertyValue =>
            {
                _context.DevicePropertyValue.Add((new NSI.EF.DevicePropertyValue()).FromDomainModel(devicePropertyValue));
            });

            _context.SaveChanges();

            return devicePingDb.DevicePingId;
        }

        public bool DeleteDevicePingById(int tenantId, int id)
        {
            if (tenantId <= 0) throw new NsiArgumentException("Invalid tenant ID");

            try
            {
                EF.DevicePing devicePing = _context.DevicePing.FirstOrDefault(x => x.DevicePingId == id && x.TenantId == tenantId);

                if (devicePing != null)
                {
                    _context.DevicePing.Remove(devicePing);
                }

                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public DevicePingDomain GetDevicePingById(int tenantId, int id)
        {
            if (tenantId <= 0) throw new NsiArgumentException("Invalid tenant ID");

            var devicePing = _context.DevicePing
                .Include(x => x.Device)
                .Include(x => x.DevicePropertyValue)
                .Include(x => x.Action)
                .Where(dp => dp.DevicePingId == id && dp.TenantId == tenantId)
                .FirstOrDefault();

            return devicePing.ToDomainModel();
        }

        public ICollection<DevicePingDomain> Search(int tenantId, Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            if (tenantId <= 0) throw new NsiArgumentException("Invalid tenant ID");

            var result = _context.DevicePing
                .Where(dp => dp.TenantId == tenantId)
                .Include(x => x.Device)
                .Include(x => x.DevicePropertyValue)
                .Include(x => x.Action)
                .DoFiltering(filterCriteria, FilterDevicePing)
                .DoSorting(sortCriteria, SortDevicePing)
                .DoPaging(paging)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        public int DevicePingsCount()
        {
            return _context.DevicePing.Count();
        }

        public DevicePingDomain LastDevicePingForDevice(int deviceId)
        {
            var result = _context.DevicePing
                .Include(x => x.Device)
                .Include(x => x.DevicePropertyValue)
                .Include(x => x.Action)
                .Where(x => x.DeviceId == deviceId)
                .OrderByDescending(x => x.DateCreated)
                .Take(1)
                .FirstOrDefault();

            return result.ToDomainModel();
        }
        public ICollection<Domain.DevicePing.DevicePropertyValue> GetAllDeviceProperties()
        {
            ICollection<Domain.DevicePing.DevicePropertyValue> listToReturn = new List<Domain.DevicePing.DevicePropertyValue>();

            foreach (EF.DevicePropertyValue dpValue in _context.DevicePropertyValue.ToList())
            {
                listToReturn.Add(dpValue.ToDomainModel());
            }

            return listToReturn;
        }

        #region Private methods
        private Expression<Func<EF.DevicePing, object>> SortDevicePing(string columnName)
        {
            Expression<Func<EF.DevicePing, object>> fnc = null;

            switch (columnName)
            {
                case "deviceName":
                    fnc = x => x.Device.Name;
                    break;
                case "dateCreated":
                    fnc = x => x.DateCreated;
                    break;
                default:
                    break;
            }

            return fnc;
        }

        private Expression<Func<EF.DevicePing, bool>> FilterDevicePing(string columnName, string filterTerm, bool isExactMatch)
        {
            Expression<Func<EF.DevicePing, bool>> fnc = null;

            if (string.IsNullOrEmpty(filterTerm) || string.IsNullOrEmpty(columnName))
            {
                fnc = x => true;
                return fnc;
            }

            switch (columnName)
            {
                case "deviceName":
                    if (!isExactMatch)
                    {
                        fnc = x => (x.Device.Name).Contains(filterTerm);
                    }
                    else
                    {
                        fnc = x => x.Device.Name == filterTerm;
                    }
                    break;
                case "deviceId":
                    fnc = x => x.Device.DeviceId == Convert.ToInt32(filterTerm);
                    break;
                case "dateCreated":
                    {
                        if (DateTime.TryParse(filterTerm, out DateTime dateTime))
                        {
                            fnc = x => DbFunctions.TruncateTime(x.DateCreated) == DbFunctions.TruncateTime(dateTime.Date);
                        }
                        else
                        {
                            fnc = x => false;
                        }
                        break;
                    }
                case "actionName":
                    if (!isExactMatch)
                    {
                        fnc = x => (x.Action.Name).Contains(filterTerm);
                    }
                    else
                    {
                        fnc = x => x.Action.Name == filterTerm;
                    }
                    break;
            }

            return fnc;
        }
        #endregion
    }
}
