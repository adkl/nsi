using NSI.Domain.RuleEngine;
using System.Collections.Generic;
using NSI.Common.Models;
using NSI.Domain.Membership;

namespace NSI.Repository.Interfaces.RuleEngine
{
    public interface IRuleRepository
    {
        ICollection<RuleDomain> GetAllRules(UserDomain user, IList<FilterCriteria> filters, Paging paging);
        ICollection<RuleDomain> GetAllByDeviceId(int deviceId);
        int AddRule(AddRuleDomain rule, UserDomain user);
        void DeleteRule(int id, UserDomain user);
        RuleDomain GetById(int ruleId);
    }
}
