using NSI.Domain.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.DeviceManagement
{
    public interface IDeviceActionManipulation
    {
        ICollection<ActionDomain> GetAllActions(int tenantId);
    }
}
