using NSI.Domain.Membership;
using NSI.Domain.RuleEngine;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class RuleActionExtension
    {
        public static RuleActionDomain ToDomainModel(this RuleAction obj)
        {
            return obj == null ? null : new RuleActionDomain()
            {
                RuleActionId = obj.RuleActionId,
                DateCreated = obj.DateCreated,
                CreatedBy = obj.CreatedBy,
                DateModified = obj.DateModified,
                ModifiedBy = obj.ModifiedBy,
                IsActive = obj.IsActive,
                Device = obj.Device.ToDomainModel(),
                Action = obj.Action.ToDomainModel()
            };
        }

        public static RuleAction FromDomainModel(this RuleAction obj, RuleActionDomain domain)
        {
            if (obj == null)
            {
                obj = new RuleAction();
            }

            obj.RuleActionId = domain.RuleActionId;
            obj.DateCreated = domain.DateCreated;
            obj.CreatedBy = domain.CreatedBy;
            obj.DateModified = domain.DateModified;
            obj.ModifiedBy = domain.ModifiedBy;
            obj.IsActive = domain.IsActive;

            return obj;
        }

        public static RuleAction FromDomainModel(this RuleAction obj, AddRuleActionDomain domain, UserDomain user)
        {
            if (obj == null)
            {
                obj = new RuleAction();
            }

            obj.ActionId = domain.ActionId;
            obj.DeviceId = domain.DeviceId;
            obj.TenantId = user.TenantId;

            return obj;
        }
    }
}
