using NSI.Common.Models;
using NSI.Domain.DevicePing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.DevicePing
{
    public interface IDevicePingManipulation
    {
        int Add(int tenantId, List<DevicePropertyValue> devicePropertyValues);
        IEnumerable<DevicePingDomain> Search(int tenantId, Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
        DevicePingDomain GetById(int tenantId, int devicePingId);
        bool Delete(int tenantId, int devicePingId);
        IEnumerable<DevicePingDomain> DevicesLastPings(int tenantId);
        int DevicePingsCount();
        string GetLastValue(int id);
    }
}
