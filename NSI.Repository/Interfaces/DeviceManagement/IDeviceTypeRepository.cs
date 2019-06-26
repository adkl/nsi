using NSI.Domain.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.DeviceManagement
{
    public interface IDeviceTypeRepository
    {
        ICollection<DeviceTypeDomain> GetAllDeviceTypes();
        DeviceTypeDomain GetDeviceTypeById(int id);
        int CreateDeviceType(DeviceTypeDomain deviceType);
        int UpdateDeviceType(DeviceTypeDomain deviceType);
        bool DeleteDeviceType(int deviceTypeId);
        ICollection<DeviceTypeDomain> GetAllActiveDeviceTypes();
        ICollection<DeviceTypeDomain> GetAllInactiveDeviceTypes();
        ICollection<DeviceTypeDomain> SearchDeviceTypes(String s);
        ICollection<DeviceTypeDomain> SearchDeviceTypes(String s, bool isActive);
    }
}
