using NSI.Domain.DeviceManagement;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.DeviceManagement
{
    public interface IDeviceManipulation
    {
        ICollection<DeviceDomain> GetAllDevices(int tenantId);
        DeviceDomain GetDeviceById(int deviceId);
        int CreateDevice(CreateDeviceDomain device, UserDomain user);
        int UpdateDevice(UpdateDeviceDomain device, UserDomain user);
        ICollection<DeviceDomain> GetAllActiveDevices(int tenantId);
        ICollection<DeviceDomain> GetAllInactiveDevices(int tenantId);
        bool DeleteDevice(int deviceId, UserDomain user);
        ICollection<DeviceDomain> SearchDevices(int tenantId,String s);
        int GetNumberOfIncidents(int deviceId, int periodInDays, int tenantId);
        ICollection<DeviceDomain> SearchDevices(int tenantId, String s, bool isActive);
    }
}
