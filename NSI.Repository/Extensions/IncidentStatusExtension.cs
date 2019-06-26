using NSI.Domain.IncidentManagement;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class IncidentStatusExtension
    {
        public static IncidentStatusDomain ToDomainModel(this IncidentStatus obj)
        {
            return obj == null ? null : new IncidentStatusDomain()
            {
                IncidentStatusId = obj.IncidentStatusId,
                Name = obj.Name,
                Code = obj.Code,
                IsActive = obj.IsActive
            };
        }

        public static IncidentStatus FromDomainModel(this IncidentStatus obj, IncidentStatusDomain domain)
        {
            if (obj == null)
            {
                obj = new IncidentStatus();
            }

            obj.IncidentStatusId = domain.IncidentStatusId;
            obj.Name = domain.Name;
            obj.Code = domain.Code;
            obj.IsActive = domain.IsActive;

            return obj;
        }
    }
}
