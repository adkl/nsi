using NSI.Domain.DevicePing;
using NSI.EF;
using System.Linq;

namespace NSI.Repository.Extensions
{
    public static class DevicePingExtension
    {
        public static DevicePingDomain ToDomainModel(this EF.DevicePing obj)
        {
            return obj == null ? null : new DevicePingDomain()
            {
                Id = obj.DevicePingId,
                TenantId = obj.TenantId,
                RuleId = obj.RuleId,
                DateCreated = obj.DateCreated,
                ActionId = obj.ActionId,
                Action = obj.Action.ToDomainModel(),
                DeviceId = obj.DeviceId,
                Device = obj.Device.ToDomainModel(),
                DevicePropertyValues = obj.DevicePropertyValue.ToDomainModelCollection()
            };
        }

        public static NSI.EF.DevicePing FromDomainModel(this NSI.EF.DevicePing obj, DevicePingDomain domain)
        {
            if (obj == null)
            {
                obj = new NSI.EF.DevicePing();
            }

            obj.DevicePingId = domain.Id;
            obj.TenantId = domain.TenantId;
            obj.RuleId = domain.RuleId;
            obj.DateCreated = domain.DateCreated;
            obj.ActionId = domain.ActionId;
            obj.DeviceId = domain.DeviceId;
          
            return obj;
        }
    }
}
