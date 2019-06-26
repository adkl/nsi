using NSI.Common.Models;
using NSI.Domain.DevicePing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.DevicePing
{
    public interface IDevicePingRepository
    {
        ICollection<DevicePingDomain> Search(int tenantId, Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
        DevicePingDomain GetDevicePingById(int tenantId, int id);
        int AddDevicePing(int tenantId, DevicePingDomain device);
        bool DeleteDevicePingById(int tenantId, int id);
        DevicePingDomain LastDevicePingForDevice(int deviceId);
        int DevicePingsCount();
        ICollection<DevicePropertyValue> GetAllDeviceProperties();
    }
}
