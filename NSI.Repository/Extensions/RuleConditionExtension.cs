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
    public static class RuleConditionExtension
    {
        public static RuleConditionDomain ToDomainModel(this RuleCondition obj)
        {
            return obj == null ? null : new RuleConditionDomain()
            {
                RuleConditionId = obj.RuleConditionId,
                ComparisonOperator = obj.ComparisonOperator,
                ComparisonValue = obj.ComparisonValue,
                DateCreated = obj.DateCreated,
                CreatedBy = obj.CreatedBy,
                DateModified = obj.DateModified,
                ModifiedBy = obj.ModifiedBy,
                IsActive = obj.IsActive,
                Device = obj.Device.ToDomainModel(),
                Property = obj.Property.ToDomainModel()
            };
        }

        public static RuleCondition FromDomainModel(this RuleCondition obj, RuleConditionDomain domain)
        {
            if (obj == null)
            {
                obj = new RuleCondition();
            }

            obj.RuleConditionId = domain.RuleConditionId;
            obj.ComparisonOperator = domain.ComparisonOperator;
            obj.ComparisonValue = domain.ComparisonValue;
            obj.DateCreated = domain.DateCreated;
            obj.CreatedBy = domain.CreatedBy;
            obj.DateModified = domain.DateModified;
            obj.ModifiedBy = domain.ModifiedBy;
            obj.IsActive = domain.IsActive;

            return obj;
        }

        public static RuleCondition FromDomainModel(this RuleCondition obj, AddRuleConditionDomain domain, UserDomain user)
        {
            if (obj == null)
            {
                obj = new RuleCondition();
            }
            
            obj.ComparisonOperator = domain.ComparisonOperator;
            obj.ComparisonValue = domain.ParameterValue;
            obj.DeviceId = domain.DeviceId;
            obj.PropertyId = domain.ParameterId;
            obj.TenantId = user.TenantId;

            return obj;
        }
    }
}
