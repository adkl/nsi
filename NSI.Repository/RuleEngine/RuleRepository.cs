using NSI.Common.Exceptions;
using NSI.Domain.RuleEngine;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Interfaces.RuleEngine;
using System.Collections.Generic;
using System.Linq;
using NSI.Common.Extensions;
using NSI.Common.Models;
using NSI.Domain.Membership;

namespace NSI.Repository.RuleEngine
{
    public class RuleRepository : IRuleRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public RuleRepository(NsiContext context)
        {
            _context = context;
        }

        public ICollection<RuleDomain> GetAllByDeviceId(int deviceId)
        {
            return _context
                .Rule
                .Where(
                    rule => rule.RuleCondition.Any(
                        condition => condition.DeviceId == deviceId
                    )
                )
                .AsEnumerable()
                .Select(rule => rule.ToDomainModel())
                .ToList();
        }

        public ICollection<RuleDomain> GetAllRules(UserDomain user, IList<FilterCriteria> filters, Paging paging)
        {
            var builder = _context
                .Rule
                .Where(rule => rule.IsDeleted != true && rule.TenantId == user.TenantId);

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (filter.ColumnName == "deviceId")
                    {
                        var deviceId = int.Parse(filter.FilterTerm);

                        builder = builder.Where(
                            rule => rule.RuleCondition.Any(
                                condition => condition.DeviceId == deviceId
                            )
                        );
                    }
                }
            }

            return builder
                .OrderBy(rule => true)
                .DoPaging(paging)
                .AsEnumerable()
                .Select(rule => rule.ToDomainModel())
                .ToList();
        }
        
        public int AddRule(AddRuleDomain rule, UserDomain user)
        {
            // Validate conditions
            foreach (var condition in rule.Conditions)
            {
                var device = _context.Device.Find(condition.DeviceId);

                if (device == null)
                {
                    throw new NsiArgumentException("Device does not exist.");
                }

                if (device.DeviceType.Property.First(property => property.PropertyId == condition.ParameterId) == null)
                {
                    throw new NsiArgumentException("Parameter is not defined for the selected device.");
                }
            }

            // Validate actions
            foreach (var action in rule.Actions)
            {
                var device = _context.Device.Find(action.DeviceId);

                if (device == null)
                {
                    throw new NsiArgumentException("Device does not exist.");
                }

                if (device.DeviceType.Action.First(current => current.ActionId == action.ActionId) == null)
                {
                    throw new NsiArgumentException("Action is not defined for the selected device.");
                }
            }
            
            // Create everything
            var ruleObject = new Rule().FromDomainModel(rule, user);

            foreach (var condition in rule.Conditions)
            {
                ruleObject.RuleCondition.Add(new RuleCondition().FromDomainModel(condition, user));
            }

            foreach (var action in rule.Actions)
            {
                ruleObject.RuleAction.Add(new RuleAction().FromDomainModel(action, user));
            }

            _context.Rule.Add(ruleObject);
            _context.SaveChanges();

            return ruleObject.RuleId;
        }

        public void DeleteRule(int id, UserDomain user)
        {
            var ruleDb = _context.Rule.FirstOrDefault(x => x.RuleId == id);

            if (ruleDb != null)
            {
                ruleDb.IsDeleted = true;
            }

            _context.SaveChanges();
        }

        public RuleDomain GetById(int ruleId)
        {
            return _context.Rule.FirstOrDefault(rule => rule.RuleId == ruleId).ToDomainModel();
        }
    }
}
