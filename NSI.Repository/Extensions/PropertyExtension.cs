using NSI.Domain.DeviceManagement;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class PropertyExtension
    {
        public static PropertyDomain ToDomainModel(this Property obj)
        {
            return obj == null ? null : new PropertyDomain()
            {
                PropertyId = obj.PropertyId,
                Name = obj.Name,
                IsActive = obj.IsActive,
            };
        }

        public static Property FromDomainModel(this Property obj, PropertyDomain domain)
        {
            if (obj == null)
            {
                obj = new Property();
            }

            obj.PropertyId = domain.PropertyId;
            obj.Name = domain.Name;
            obj.IsActive = domain.IsActive;

            return obj;

        }
    }
}
