using NSI.Domain.DeviceManagement;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class DeviceTypeExtension
    {
        public static DeviceTypeDomain ToDomainModel(this DeviceType obj)
        {
            return obj == null ? null : new DeviceTypeDomain()
            {
                DeviceTypeId = obj.DeviceTypeId,
                Name = obj.Name,
                Code = obj.Code,
                IsActive = obj.IsActive,
                Properties = obj.Property.Select(x => x.ToDomainModel()).ToList(),
                Actions = obj.Action.Select(x => x.ToDomainModel()).ToList()
            };
        }

        public static DeviceType FromDomainModel(this DeviceType obj, DeviceTypeDomain domain)
        {
            if (obj == null)
            {
                obj = new DeviceType();
            }

            obj.DeviceTypeId = domain.DeviceTypeId;
            obj.Name = domain.Name;
            obj.Code = domain.Code;
            obj.IsActive = domain.IsActive;

            return obj;
            
        }
    }
}
