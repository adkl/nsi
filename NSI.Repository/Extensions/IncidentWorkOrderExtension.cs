using NSI.Domain.IncidentManagement;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class IncidentWorkOrderExtension
    {
        public static IncidentWorkOrderDomain ToDomainModel(this WorkOrder obj)
        {
            return obj == null ? null : new IncidentWorkOrderDomain()
            {
                WorkOrderId = obj.WorkOrderId,
                DateCreated = obj.DateCreated,
                CreatedBy = obj.CreatedBy,
                ModifiedBy = obj.ModifiedBy,
                DateModified = obj.DateModified,
                TenantId = obj.TenantId,
                IncidentId = obj.IncidentId,
                IncidentSettlementId = obj.IncidentSettlementId
            };
        }

        public static WorkOrder FromDomainModel(this WorkOrder obj, IncidentWorkOrderDomain domain)
        {
            if (obj == null)
            {
                obj = new WorkOrder();
            }

            obj.WorkOrderId = domain.WorkOrderId;
            obj.DateCreated = domain.DateCreated;
            obj.CreatedBy = domain.CreatedBy;
            obj.ModifiedBy = domain.ModifiedBy;
            obj.DateModified = domain.DateModified;
            obj.TenantId = domain.TenantId;
            obj.IncidentId = domain.IncidentId;
            obj.IncidentSettlementId = domain.IncidentSettlementId;

            return obj;
        }

    }
}
