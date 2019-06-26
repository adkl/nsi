using NSI.Domain.DeviceManagement;
using NSI.Domain.Membership;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class DeviceExtension
    {
        public static DeviceDomain ToDomainModel(this Device obj)
        {
            return obj == null ? null : new DeviceDomain()
            {
                DeviceId = obj.DeviceId,
                Identifier = obj.Identifier,
                Description = obj.Description,
                Name = obj.Name,
                IsActive = obj.IsActive,
                IsDeleted = obj.IsDeleted,
                DeviceTypeId = obj.DeviceTypeId,
                DeviceStatusId = obj.DeviceStatusId,
                TenantId = obj.TenantId,
                DeviceType = obj.DeviceType.ToDomainModel(),
                DeviceStatus = obj.DeviceStatus.ToDomainModel(),
                AuditTrail = new AuditTrailDomain
                {
                    CreatedyById = obj.CreatedBy,
                    DateCreated = obj.DateCreated,
                    ModifiedById = obj.ModifiedBy,
                    DateModified = obj.DateModified
                },
                DeviceImage = obj.DeviceImage
            };
        }
       
        public static Device FromDomainModel(this Device obj, DeviceDomain domain)
        {
            if (obj == null)
            {
                obj = new Device();
            }

            obj.DeviceId = domain.DeviceId;
            obj.Identifier = domain.Identifier;
            obj.Description = domain.Description;
            obj.Name = domain.Name;
            obj.IsActive = domain.IsActive;
            obj.IsDeleted = domain.IsDeleted;
            obj.DeviceTypeId = domain.DeviceTypeId;
            obj.DeviceStatusId = domain.DeviceStatusId;
            obj.TenantId = domain.TenantId;
            obj.CreatedBy = domain.AuditTrail.CreatedyById;
            obj.DateCreated = domain.AuditTrail.DateCreated;
            obj.DeviceImage = domain.DeviceImage;

        return obj;
            
        }

        public static Device FromDomainModel(this Device obj, UpdateDeviceDomain domain, UserDomain user)
        {
            if (obj == null)
            {
                obj = new Device();
            }
            
            obj.Description = domain.Description;
            obj.Name = domain.Name;
            obj.IsActive = domain.IsActive;
            obj.IsDeleted = domain.IsDeleted;
            obj.DeviceTypeId = domain.DeviceTypeId;
            obj.ModifiedBy = user.Id;
            obj.DateModified = DateTime.Now;
            obj.DeviceImage = domain.DeviceImage;

            return obj;

        }

        public static Device FromDomainModel(this Device obj, CreateDeviceDomain domain, UserDomain user)
        {
            if (obj == null)
            {
                obj = new Device();
            }

            obj.Identifier = Guid.NewGuid();
            obj.Description = domain.Description;
            obj.Name = domain.Name;
            obj.IsActive = true;
            obj.IsDeleted = false;
            obj.DeviceTypeId = domain.DeviceTypeId;
            obj.CreatedBy = user.Id;
            obj.DateCreated = DateTime.Now;
            obj.TenantId = user.TenantId;
            obj.DeviceImage = domain.DeviceImage;

            return obj;

        }
    }
}
