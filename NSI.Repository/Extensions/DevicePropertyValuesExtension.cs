using NSI.Domain.DevicePing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class DevicePropertyValuesExtension
    {
        public static DevicePropertyValue ToDomainModel(this EF.DevicePropertyValue obj)
        {
            return obj == null ? null : new DevicePropertyValue()
            {
                Id = obj.DevicePropertyValueId,
                DateCreated = obj.DateCreated,
                DeviceId = obj.DeviceId,
                Device = obj.Device.ToDomainModel(),
                DevicePingId = obj.DevicePingId,
                PropertyId = obj.PropertyId,
                Value = obj.PropertyValue,
                Property = obj.Property.ToDomainModel()                
            };
        }

        public static List<DevicePropertyValue> ToDomainModelCollection(this ICollection<EF.DevicePropertyValue> collection)
        {
            if(collection == null)
            {
                return null;
            }

            List<DevicePropertyValue> devicePropertyValues = new List<DevicePropertyValue>();
            collection.ToList().ForEach(pv => devicePropertyValues.Add(pv.ToDomainModel()));
            return devicePropertyValues;
        }

        public static EF.DevicePropertyValue FromDomainModel(this EF.DevicePropertyValue obj, DevicePropertyValue domain)
        {
            if (obj == null)
            {
                obj = new EF.DevicePropertyValue();
            }

            obj.DevicePropertyValueId = domain.Id;
            obj.DateCreated = domain.DateCreated;
            obj.DeviceId = domain.DeviceId;
            obj.DevicePingId = domain.DevicePingId;
            obj.PropertyId = domain.PropertyId;
            obj.PropertyValue = domain.Value;
            obj.PropertyId = domain.PropertyId;

            return obj;
        }
    }
}