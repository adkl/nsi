using NSI.Domain.DevicePing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.DevicePing
{
    public interface IPingDeviceManipulation
    {
        void PingDevice(int tenantId, PingDeviceDomain pingDeviceDomain);
    }
}
