using NSI.Domain.IncidentManagement;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class IncidentSettlementExtension
    {
        public static IncidentSettlementDomain ToDomainModel(this IncidentSettlement obj)
        {
            return obj == null ? null : new IncidentSettlementDomain()
            {
                IncidentSettlementId = obj.IncidentSettlementId,
                Description = obj.Description,
                FullText = obj.FullText,
                DateSettled = obj.DateSettled,
                DateCreated = obj.DateCreated,
                ModifiedBy = obj.ModifiedBy,
                DateModified = obj.DateModified,
                TenantId = obj.TenantId,
                IncidentStatusId = obj.IncidentStatusId
            };
    }
        public static IncidentSettlement FromDomainModel(this IncidentSettlement obj, IncidentSettlementDomain domain)
        {
            if (obj == null)
            {
                obj = new IncidentSettlement();
            }

            obj.IncidentSettlementId = domain.IncidentSettlementId;
            obj.Description = domain.Description;
            obj.FullText = domain.FullText;
            obj.DateSettled = domain.DateSettled;
            obj.DateCreated = domain.DateCreated;
            obj.ModifiedBy = domain.ModifiedBy;
            obj.DateModified = domain.DateModified;
            obj.TenantId = domain.TenantId;
            obj.IncidentStatusId = domain.IncidentStatusId;

            return obj;
        }
    }
}
