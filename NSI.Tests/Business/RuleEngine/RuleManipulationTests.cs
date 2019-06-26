using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks;
using NSI.BusinessLogic.RuleEngine;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Common.Models;
using NSI.Domain.Membership;
using NSI.Domain.RuleEngine;
using NSI.Repository.Interfaces.RuleEngine;
using System;
using System.Collections.Generic;

namespace NSI.Tests.Business
{
    [TestClass]
    public class RuleManipulationTests
    {
        private Mock<IRuleRepository> _RuleRepositoryMock;
        private RuleManipulation _ruleManipulation;
        private UserDomain user = new UserDomain { TenantId = 1};
        private RuleDomain rule = new RuleDomain { RuleId = 1, TenantId = 1 };
        
        

        [TestInitialize]
        public void Initialize()
        {
            _RuleRepositoryMock = RuleRepositoryMock.GetRuleRepositoryMock();
            _ruleManipulation = new RuleManipulation(_RuleRepositoryMock.Object);
        }

        [TestMethod, TestCategory("Rule - Delete Rule Success")]
        public void Delete_Rule_Success()
        {

            _ruleManipulation.DeleteRule(1, user);
        }

        [TestMethod, TestCategory("Rule - DeleteRuleThrowsAnErrorIfArgumentIsInvalid")]
        [ExpectedException(typeof(NsiNotFoundException))]
        public void Delete_Rule_Fails()
        {

            _ruleManipulation.DeleteRule(-1, user);
        }

        [TestMethod, TestCategory("Rule - GetAllRules")]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetAllRules_ThrowsException_When_Paging_Is_Not_Correct()
        {
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


            List<RuleDomain> allRules = (List<RuleDomain>)_ruleManipulation.GetAllRules(user, filtersForRules, pagingForRules);

            Assert.AreEqual(allRules.Count, 2);
        }

        [TestMethod, TestCategory("Rule - AddRule")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void AddRule_Fail_NullValue()
        {
            _ruleManipulation.AddRule(null, user);
        }

        [TestMethod, TestCategory("Incident - AddRule")]
        public void AddRule_Success_ValidRequest()
        {

            List<AddRuleConditionDomain> conditions = new List<AddRuleConditionDomain>();
            conditions.Add(new NSI.Domain.RuleEngine.AddRuleConditionDomain
            {
                DeviceId = 1,
                ParameterId = 1,
                ComparisonOperator = "<",
                ParameterValue = "1"
            });

            List<AddRuleActionDomain> actions = new List<AddRuleActionDomain>();
            actions.Add(new NSI.Domain.RuleEngine.AddRuleActionDomain
            {
                DeviceId = 1,
                ActionId = 1
            });

            Domain.RuleEngine.AddRuleDomain request = new NSI.Domain.RuleEngine.AddRuleDomain
            {
                Name = "TestName",
                Description = "Test description",
                Conditions = conditions,
                Actions = actions

            };

            int id = _ruleManipulation.AddRule(request, user);
            Assert.AreEqual(0, id);
        }
    }
}
