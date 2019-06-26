using NSI.Domain.IncidentManagement;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class IncidentTypeExtension
    {
        public static IncidentTypeDomain ToDomainModel(this IncidentType obj)
        {
            return obj == null ? null : new IncidentTypeDomain()
            {
            IncidentTypeId = obj.IncidentTypeId,
            Name = obj.Name,
            Code = obj.Code,
            IsActive = obj.IsActive
    };
        }

        public static IncidentType FromDomainModel(this IncidentType obj, IncidentTypeDomain domain)
        {
            if (obj == null)
            {
                obj = new IncidentType();
            }

            obj.IncidentTypeId = domain.IncidentTypeId;
            obj.Name = domain.Name;
            obj.Code = domain.Code;
            obj.IsActive = domain.IsActive;

            return obj;
        }
    }
}
