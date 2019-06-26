using NSI.Domain.RuleEngine;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class RuleActionParameterExtension
    {
        public static RuleActionParameterDomain ToDomainModel(this RuleActionParameter obj)
        {
            return obj == null ? null : new RuleActionParameterDomain()
            {
                RuleActionParameterId = obj.RuleActionParameterId,
                ParameterValue = obj.ParameterValue,
                DateCreated = obj.DateCreated,
                CreatedBy = obj.CreatedBy,
                DateModified = obj.DateModified,
                ModifiedBy = obj.ModifiedBy,
                IsActive = obj.IsActive
            };
        }

        public static RuleActionParameter FromDomainModel(this RuleActionParameter obj, RuleActionParameterDomain domain)
        {
            if (obj == null)
            {
                obj = new RuleActionParameter();
            }

            obj.RuleActionParameterId = domain.RuleActionParameterId;
            obj.ParameterValue = domain.ParameterValue;
            obj.DateCreated = domain.DateCreated;
            obj.CreatedBy = domain.CreatedBy;
            obj.DateModified = domain.DateModified;
            obj.ModifiedBy = domain.ModifiedBy;
            obj.IsActive = domain.IsActive;

            return obj;
        }
    }
}
