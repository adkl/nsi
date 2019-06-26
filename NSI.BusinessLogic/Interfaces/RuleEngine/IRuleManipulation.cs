using NSI.Domain.RuleEngine;
using System.Collections.Generic;
using NSI.Common.Models;
using NSI.Domain.Membership;

namespace NSI.BusinessLogic.Interfaces.RuleEngine
{
    public interface IRuleManipulation
    {
        ICollection<RuleDomain> GetAllRules(UserDomain user, IList<FilterCriteria> filters, Paging paging);
        int AddRule(AddRuleDomain rule, UserDomain user);
        void DeleteRule(int id, UserDomain user);
    }
}
