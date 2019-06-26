using NSI.Common.Exceptions;
using NSI.Common.Resources.DeviceManagement;
using NSI.Domain.DeviceManagement;
using NSI.Domain.Membership;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Interfaces.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.DeviceManagement
{
    public class DeviceRepository : IDeviceRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public DeviceRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all devices for specific tenant id
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public ICollection<DeviceDomain> GetAllDevices(int tenantId)
        {
            if (tenantId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            ICollection<DeviceDomain> listToReturn = new List<DeviceDomain>();

            var result = _context.Device.Where(x => x.TenantId == tenantId && !x.IsDeleted).OrderByDescending(x => x.IsActive).ThenByDescending(x => x.DateCreated).ToList();

            if (result == null) throw new NsiProcessingException(DeviceMessages.UnexpectedProblem);

            foreach (Device device in result)
            {
                listToReturn.Add(device.ToDomainModel());
            }

            return listToReturn;
        }

        /// <summary>
        /// Get device by id
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public DeviceDomain GetDeviceById(int deviceId)
        {
            if (deviceId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidId);

            var result = _context.Device.FirstOrDefault(x => x.DeviceId == deviceId && !x.IsDeleted).ToDomainModel();

            return result;
        }

        /// <summary>
        /// Create device
        /// </summary>
        /// <param name="device"></param>
        /// <param name="user">This parameter is provided so that we can know which user created device.</param>
        /// <returns></returns>
        public int CreateDevice(CreateDeviceDomain device, UserDomain user)
        {

            if (device == null || user == null) throw new NsiArgumentNullException(DeviceMessages.DeviceInvalidArgument);
            DeviceStatus newStatus = _context.DeviceStatus.Where(x => x.Name == "New").FirstOrDefault();

            if (newStatus == null) throw new NsiNotFoundException(DeviceMessages.InvalidDeviceStatus);

            if (!_context.DeviceType.Any(x => x.DeviceTypeId == device.DeviceTypeId))
            {
                throw new NsiArgumentException(DeviceTypeMessages.DeviceTypeInvalidId);
            }

            if (!_context.DeviceType.Any(x => x.DeviceTypeId == device.DeviceTypeId && x.IsActive))
            {
                throw new NsiArgumentException(DeviceTypeMessages.DeviceTypeNotActive);
            }

            var deviceDb = new Device().FromDomainModel(device, user);
            deviceDb.DeviceStatusId = newStatus.DeviceStatusId;

            _context.Device.Add(deviceDb);
            _context.SaveChanges();

            return deviceDb.DeviceId;
        }

        /// <summary>
        /// Update device
        /// </summary>
        /// <param name="device"></param>
        /// <param name="user">This parameter is provided so that we can know which user modified device.</param>
        /// <returns></returns>
        public int UpdateDevice(UpdateDeviceDomain device, UserDomain user)
        {
            if (device == null || user == null) throw new NsiArgumentNullException(DeviceMessages.DeviceInvalidArgument);

            if (!_context.DeviceType.Any(x => x.DeviceTypeId == device.DeviceTypeId))
            {
                throw new NsiArgumentException(DeviceTypeMessages.DeviceTypeInvalidId);
            }

            if (!_context.DeviceType.Any(x => x.DeviceTypeId == device.DeviceTypeId && x.IsActive))
            {
                throw new NsiArgumentException(DeviceTypeMessages.DeviceTypeNotActive);
            }

            var deviceDb = _context.Device.Where(x => x.DeviceId == device.DeviceId).FirstOrDefault().FromDomainModel(device, user);

            if (deviceDb == null) throw new NsiNotFoundException(DeviceMessages.DeviceNotFound);

            _context.SaveChanges();

            return deviceDb.DeviceId;
        }

        /// <summary>
        /// Get all active devices for specific tenant id
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public ICollection<DeviceDomain> GetAllActiveDevices(int tenantId)
        {
            if (tenantId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            ICollection<DeviceDomain> listToReturn = new List<DeviceDomain>();

            var result = _context.Device.Where(x => x.TenantId == tenantId && !x.IsDeleted && x.IsActive).OrderByDescending(x => x.DateCreated).ToList();

            if (result == null) throw new NsiProcessingException(DeviceMessages.UnexpectedProblem);

            foreach (Device device in result)
            {
                listToReturn.Add(device.ToDomainModel());
            }

            return listToReturn;
        }

        /// <summary>
        /// Get all inactive devices for specific tenant id
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public ICollection<DeviceDomain> GetAllInactiveDevices(int tenantId)
        {
            if (tenantId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            ICollection<DeviceDomain> listToReturn = new List<DeviceDomain>();

            var result = _context.Device.Where(x => x.TenantId == tenantId && !x.IsDeleted && !x.IsActive).OrderByDescending(x => x.DateCreated).ToList();

            if (result == null) throw new NsiProcessingException(DeviceMessages.UnexpectedProblem);

            foreach (Device device in result)
            {
                listToReturn.Add(device.ToDomainModel());
            }

            return listToReturn;
        }

        /// <summary>
        /// Delete device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="user">This parameter is provided so that we can know which user deleted device.</param>
        /// <returns></returns>
        public bool DeleteDevice(int deviceId, UserDomain user)
        {
            if (deviceId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidId);

            var deviceDb = _context.Device.Where(x => x.DeviceId == deviceId).FirstOrDefault();

            if (deviceDb == null) return false;

            if (deviceDb.TenantId != user.TenantId)
            {
                throw new NsiNotAuthorizedException(DeviceMessages.NotAuthorizedAction);
            }

            deviceDb.IsActive = false;
            deviceDb.IsDeleted = true;
            deviceDb.ModifiedBy = user.Id;
            deviceDb.DateModified = DateTime.Now;

            _context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Search devices
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public ICollection<DeviceDomain> SearchDevices(int tenantId, String s)
        {
            if (tenantId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            ICollection<DeviceDomain> listToReturn = new List<DeviceDomain>();

            var result = _context.Device.Where(x => x.TenantId == tenantId && x.Name.Contains(s) && !x.IsDeleted).OrderByDescending(x => x.IsActive).ThenByDescending(x => x.DateCreated).ToList();

            if (result == null) throw new NsiProcessingException(DeviceMessages.UnexpectedProblem);

            foreach (Device device in result)
            {
                listToReturn.Add(device.ToDomainModel());
            }

            return listToReturn;
        }

        /// <summary>
        /// Search devices with filter applied
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="s"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public ICollection<DeviceDomain> SearchDevices(int tenantId, String s, bool isActive)
        {
            if (tenantId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            ICollection<DeviceDomain> listToReturn = new List<DeviceDomain>();

            var result = _context.Device.Where(x => x.TenantId == tenantId && x.Name.Contains(s) && !x.IsDeleted && x.IsActive == isActive).OrderByDescending(x => x.DateCreated).ToList();

            if (result == null) throw new NsiProcessingException(DeviceMessages.UnexpectedProblem);

            foreach (Device device in result)
            {
                listToReturn.Add(device.ToDomainModel());
            }

            return listToReturn;
        }

    }
}
