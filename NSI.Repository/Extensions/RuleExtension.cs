using NSI.Domain.RuleEngine;
using NSI.EF;
using System.Linq;
using NSI.Domain.Membership;

namespace NSI.Repository.Extensions
{
    public static class RuleExtension
    {
        public static RuleDomain ToDomainModel(this Rule obj)
        {
            return obj == null ? null : new RuleDomain()
            {
                RuleId = obj.RuleId,
                Name = obj.Name,
                Description = obj.Description,
                DateCreated = obj.DateCreated,
                CreatedBy = obj.CreatedBy,
                DateModified = obj.DateModified,
                ModifiedBy = obj.ModifiedBy,
                IsActive = obj.IsActive,
                IsDeleted = obj.IsDeleted,
                Conditions = obj.RuleCondition.Select(x => x.ToDomainModel()).ToList(),
                Actions = obj.RuleAction.Select(x => x.ToDomainModel()).ToList(),
                TenantId = obj.TenantId
            };
        }

        public static Rule FromDomainModel(this Rule obj, RuleDomain domain)
        {
            if (obj == null)
            {
                obj = new Rule();
            }

            obj.RuleId = domain.RuleId;
            obj.Name = domain.Name;
            obj.Description = domain.Description;
            obj.DateCreated = domain.DateCreated;
            obj.CreatedBy = domain.CreatedBy;
            obj.DateModified = domain.DateModified;
            obj.ModifiedBy = domain.ModifiedBy;
            obj.IsActive = domain.IsActive;
            obj.IsDeleted = domain.IsDeleted;

            return obj;
        }

        public static Rule FromDomainModel(this Rule obj, AddRuleDomain rule, UserDomain user)
        {
            if (obj == null)
            {
                obj = new Rule();
            }
            
            obj.Name = rule.Name;
            obj.Description = rule.Description;
            obj.TenantId = user.TenantId;

            return obj;
        }
    }
}
