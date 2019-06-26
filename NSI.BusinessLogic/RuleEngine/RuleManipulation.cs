using NSI.BusinessLogic.Interfaces.RuleEngine;
using NSI.Domain.RuleEngine;
using NSI.Repository.Interfaces.RuleEngine;
using System.Collections.Generic;
using NSI.Common.Models;
using NSI.Common.Exceptions;
using NSI.Common.Resources;
using NSI.Domain.Membership;

namespace NSI.BusinessLogic.RuleEngine
{
    public class RuleManipulation : IRuleManipulation
    {
        private readonly IRuleRepository _ruleRepository;

        public RuleManipulation(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }

        public ICollection<RuleDomain> GetAllRules(UserDomain user, IList<FilterCriteria> filters, Paging paging)
        {
            return _ruleRepository.GetAllRules(user, filters, paging);
        }

        public int AddRule(AddRuleDomain rule, UserDomain user)
        {
            if (rule == null)
            {
                throw new NsiArgumentException(ExceptionMessages.ArgumentException);
            }

            return _ruleRepository.AddRule(rule, user);
        }

        public void DeleteRule(int id, UserDomain user)
        {
            var rule = _ruleRepository.GetById(id);

            if (rule == null || rule.TenantId != user.TenantId)
            {
                throw new NsiNotFoundException("Unable to find the rule under the current account.");
            }

            _ruleRepository.DeleteRule(id, user);
        }
    }
}
