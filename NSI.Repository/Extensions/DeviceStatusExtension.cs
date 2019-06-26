using NSI.Domain.DeviceManagement;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class DeviceStatusExtension
    {
        public static DeviceStatusDomain ToDomainModel(this DeviceStatus obj)
        {
            return obj == null ? null : new DeviceStatusDomain()
            {
                DeviceStatusId = obj.DeviceStatusId,
                Name = obj.Name,
                Code = obj.Code
            };
        }

        public static DeviceStatus FromDomainModel(this DeviceStatus obj, DeviceStatusDomain domain)
        {
            if (obj == null)
            {
                obj = new DeviceStatus();
            }

            obj.DeviceStatusId = domain.DeviceStatusId;
            obj.Name = domain.Name;
            obj.Code = domain.Code;

            return obj;

        }
    }
}
