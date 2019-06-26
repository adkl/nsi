using Moq;
using NSI.Common.Models;
using NSI.Domain.Membership;
using NSI.Domain.RuleEngine;
using NSI.Repository.Interfaces.RuleEngine;
using System.Collections.Generic;

namespace Nsi.TestsCore.Mocks
{
    public static class RuleRepositoryMock
    {
        public static Mock<IRuleRepository> GetRuleRepositoryMock()
        {
            var ruleRepository = new Mock<IRuleRepository> { CallBase = false };
            UserDomain user = new UserDomain { TenantId = 1 };

            List<FilterCriteria> filtersForRules = new List<FilterCriteria>();
            FilterCriteria filterCriteriaForRule = new FilterCriteria();
            filterCriteriaForRule.ColumnName = "Name";
            filterCriteriaForRule.FilterTerm = "";
            filterCriteriaForRule.IsExactMatch = false;
            filtersForRules.Add(filterCriteriaForRule);

            Paging pagingForRules = new Paging();
            pagingForRules.Page = 1;
            pagingForRules.Pages = 1;
            pagingForRules.RecordsPerPage = 2;
            pagingForRules.TotalRecords = 2;

            ruleRepository.Setup(x => x.GetById(1)).Returns(
              new RuleDomain
              {
                  TenantId = 1,
                  RuleId = 1
              }
            );

            ruleRepository.Setup(x => x.GetAllRules(user, filtersForRules, pagingForRules)).Returns(
               new List<RuleDomain> {
                    new RuleDomain(), new RuleDomain()
                }
              );


            ruleRepository.Setup(x => x.DeleteRule(-1, user));

            ruleRepository.Setup(x => x.AddRule(It.IsAny<NSI.Domain.RuleEngine.AddRuleDomain>(), user)).Returns(0);

            return ruleRepository;
        }
    }
}