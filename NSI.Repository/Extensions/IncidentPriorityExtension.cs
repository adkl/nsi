using NSI.Domain.IncidentManagement;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class IncidentPriorityExtension
    {
        public static IncidentPriorityDomain ToDomainModel(this Priority obj)
        {
            return obj == null ? null : new IncidentPriorityDomain()
            {
                PriorityId = obj.PriorityId,
                Name = obj.Name,
                Code = obj.Code,
                ColorCode = obj.ColorCode,
                IsActive = obj.IsActive,
                IconPath = obj.IconPath
    };
        }

        public static Priority FromDomainModel(this Priority obj, IncidentPriorityDomain domain)
        {
            if (obj == null)
            {
                obj = new Priority();
            }

            obj.PriorityId = domain.PriorityId;
            obj.Name = domain.Name;
            obj.Code = domain.Code;
            obj.ColorCode = domain.ColorCode;
            obj.IsActive = domain.IsActive;
            obj.IconPath = domain.IconPath;

            return obj;
        }
    }
}
