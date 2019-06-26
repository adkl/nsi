using NSI.Domain.IncidentManagement;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class IncidentExtension
    {
        public static IncidentDomain ToDomainModel(this Incident obj)
        {
            return obj == null ? null : new IncidentDomain()
            {
                IncidentId = obj.IncidentId,
                DateCreated = obj.DateCreated,
                CreatedBy = obj.CreatedBy,
                ModifiedBy = obj.ModifiedBy,
                DateModified = obj.DateModified,
                TenantId = obj.TenantId,
                Tenant = obj.Tenant.ToDomainModel(),
                IncidentStatus = obj.IncidentStatus.ToDomainModel(),
                Device = obj.Device.ToDomainModel(),
                Priority = obj.Priority.ToDomainModel(),
                IncidentType = obj.IncidentType.ToDomainModel(),
                Reporter = obj.UserInfo.ToDomainModel(),
                Assignee = obj.UserInfo1.ToDomainModel()
            };
        }

        public static Incident FromDomainModel(this Incident obj, POSTIncidentDomain domain)
        {
            if (obj == null)
            {
                obj = new Incident();
            }

            obj.IncidentId = domain.IncidentId;
            obj.DateCreated = domain.DateCreated;
            obj.CreatedBy = domain.CreatedBy;
            obj.ModifiedBy = domain.ModifiedBy;
            obj.DateModified = domain.DateModified;
            obj.TenantId = domain.TenantId;
            obj.IncidentStatusId = domain.IncidentStatus;
            obj.DeviceId = domain.DeviceId;
            obj.PriorityId = domain.Priority;
            obj.IncidentTypeId = domain.IncidentType;
            obj.ReporterId = domain.ReporterId;
            obj.AssigneeId = domain.AssigneeId;

            return obj;
        }
    }
}
