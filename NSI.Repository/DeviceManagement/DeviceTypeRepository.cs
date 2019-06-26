using NSI.Common.Exceptions;
using NSI.Common.Resources.DeviceManagement;
using NSI.Domain.DeviceManagement;
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
    public class DeviceTypeRepository : IDeviceTypeRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public DeviceTypeRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all device types
        /// </summary>
        /// <returns></returns>
        public ICollection<DeviceTypeDomain> GetAllDeviceTypes()
        {
            ICollection<DeviceTypeDomain> listToReturn = new List<DeviceTypeDomain>();

            foreach (DeviceType deviceType in _context.DeviceType.ToList())
            {
                listToReturn.Add(deviceType.ToDomainModel());
            }

            return listToReturn;
        }

        /// <summary>
        /// Get device type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DeviceTypeDomain GetDeviceTypeById(int id)
        {
            if (id <= 0)
            {
                throw new NsiArgumentException(DeviceTypeMessages.DeviceTypeInvalidArgument);
            }

            var result = _context.DeviceType.FirstOrDefault(x => x.DeviceTypeId == id).ToDomainModel();

            return result;
        }

        /// <summary>
        /// Create device type
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public int CreateDeviceType(DeviceTypeDomain deviceType)
        {
            if (deviceType == null)
            {
                throw new NsiArgumentNullException(DeviceTypeMessages.DeviceTypeInvalidArgument);
            }

            var deviceTypedb = new DeviceType().FromDomainModel(deviceType);
            var actionIds = deviceType.Actions.Select(x => x.ActionId).ToList();
            var propertyIds = deviceType.Properties.Select(x => x.PropertyId).ToList();

            deviceTypedb.Action = _context.Action.Where(x => actionIds.Contains(x.ActionId)).ToList();
            deviceTypedb.Property = _context.Property.Where(x => propertyIds.Contains(x.PropertyId)).ToList();

            _context.DeviceType.Add(deviceTypedb);
            _context.SaveChanges();

            return deviceTypedb.DeviceTypeId;
        }

        /// <summary>
        /// Update device type
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public int UpdateDeviceType(DeviceTypeDomain deviceType)
        {
            if (deviceType == null) throw new NsiArgumentNullException(DeviceMessages.DeviceInvalidArgument);

            var deviceTypeDb = _context.DeviceType.Where(x => x.DeviceTypeId == deviceType.DeviceTypeId).FirstOrDefault().FromDomainModel(deviceType);

            if (deviceTypeDb == null) throw new NsiNotFoundException(DeviceMessages.DeviceNotFound);

            var actionIds = deviceType.Actions.Select(x => x.ActionId).ToList();
            var propertyIds = deviceType.Properties.Select(x => x.PropertyId).ToList();

            foreach (EF.Action action in deviceTypeDb.Action.ToList())
            {
                deviceTypeDb.Action.Remove(action);
            }

            foreach (Property property in deviceTypeDb.Property.ToList())
            {
                deviceTypeDb.Property.Remove(property);
            }

            deviceTypeDb.Action = _context.Action.Where(x => actionIds.Contains(x.ActionId)).ToList();
            deviceTypeDb.Property = _context.Property.Where(x => propertyIds.Contains(x.PropertyId)).ToList();

            _context.SaveChanges();

            return deviceTypeDb.DeviceTypeId;
        }

        /// <summary>
        /// Delete device type
        /// </summary>
        /// <param name="deviceTypeId"></param>
        /// <returns></returns>
        public bool DeleteDeviceType(int deviceTypeId)
        {
            if (deviceTypeId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            var deviceTypeDb = _context.DeviceType.Where(x => x.DeviceTypeId == deviceTypeId).FirstOrDefault();

            if (deviceTypeDb == null) return false;


            deviceTypeDb.IsActive = false;

            var devices = _context.Device.Where(x => x.DeviceTypeId == deviceTypeId).ToList();

            foreach (Device device in devices)
            {
                device.IsActive = false;
                device.IsDeleted = true;
            }

            _context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Get all active device types
        /// </summary>
        /// <returns></returns>
        public ICollection<DeviceTypeDomain> GetAllActiveDeviceTypes()
        {
            ICollection<DeviceTypeDomain> listToReturn = new List<DeviceTypeDomain>();

            var result = _context.DeviceType.Where(x => x.IsActive).ToList();

            foreach (DeviceType deviceType in result)
            {
                listToReturn.Add(deviceType.ToDomainModel());
            }

            return listToReturn;
        }

        /// <summary>
        /// Get all inactive device types
        /// </summary>
        /// <returns></returns>
        public ICollection<DeviceTypeDomain> GetAllInactiveDeviceTypes()
        {
            ICollection<DeviceTypeDomain> listToReturn = new List<DeviceTypeDomain>();

            var result = _context.DeviceType.Where(x => !x.IsActive).ToList();

            foreach (DeviceType deviceType in result)
            {
                listToReturn.Add(deviceType.ToDomainModel());
            }

            return listToReturn;
        }

        /// <summary>
        /// Search device types
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public ICollection<DeviceTypeDomain> SearchDeviceTypes(String s)
        {
            ICollection<DeviceTypeDomain> listToReturn = new List<DeviceTypeDomain>();

            var result = _context.DeviceType.Where(x => x.Name.Contains(s)).OrderBy(x => x.Name).ToList();

            if (result == null) throw new NsiProcessingException(DeviceTypeMessages.UnexpectedProblem);

            foreach (DeviceType devicetype in result)
            {
                listToReturn.Add(devicetype.ToDomainModel());
            }

            return listToReturn;
        }

        /// <summary>
        /// Search device types with filter applied
        /// </summary>
        /// <param name="s"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public ICollection<DeviceTypeDomain> SearchDeviceTypes(String s, bool isActive)
        {
            ICollection<DeviceTypeDomain> listToReturn = new List<DeviceTypeDomain>();

            var result = _context.DeviceType.Where(x => x.Name.Contains(s) && x.IsActive == isActive).OrderBy(x => x.Name).ToList();

            if (result == null) throw new NsiProcessingException(DeviceTypeMessages.UnexpectedProblem);

            foreach (DeviceType devicetype in result)
            {
                listToReturn.Add(devicetype.ToDomainModel());
            }

            return listToReturn;
        }
    }
}
