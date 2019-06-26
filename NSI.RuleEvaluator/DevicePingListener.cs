using System;
using System.Collections.Generic;
using System.Linq;
using NSI.Domain.DevicePing;
using NSI.Domain.IncidentManagement;
using NSI.Domain.RuleEngine;
using NSI.Repository.Interfaces.DevicePing;
using NSI.Repository.Interfaces.IncidentManagement;
using NSI.Repository.Interfaces.RuleEngine;

namespace NSI.RuleEvaluator
{
    public class DevicePingListener
    {
        private static DevicePingListener _instance;

        private readonly IRuleRepository _ruleRepository;
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentTypeRepository _incidentTypeRepository;

        private readonly RuleEvaluator _ruleEvaluator;

        public static DevicePingListener GetInstance()
        {
            return _instance;
        }

        public static void MakeInstance(
            IRuleRepository ruleRepository,
            IIncidentRepository incidentRepository,
            IDevicePingRepository devicePingRepository,
            IIncidentTypeRepository incidentTypeRepository
        ) {
            _instance = new DevicePingListener(
                ruleRepository,
                incidentRepository,
                devicePingRepository,
                incidentTypeRepository
            );
        }

        private DevicePingListener(
            IRuleRepository ruleRepository,
            IIncidentRepository incidentRepository,
            IDevicePingRepository devicePingRepository,
            IIncidentTypeRepository incidentTypeRepository
        )
        {
            _ruleRepository = ruleRepository;
            _incidentRepository = incidentRepository;
            _incidentTypeRepository = incidentTypeRepository;

            _ruleEvaluator = new RuleEvaluator(devicePingRepository);
        }

        public void EvaluateAll(DevicePingDomain devicePing)
        {
            IEnumerable<RuleDomain> rules = _ruleRepository.GetAllByDeviceId(devicePing.DeviceId);

            foreach (var rule in rules)
            {
                Evaluate(rule, devicePing);
            }
        }

        private void Evaluate(RuleDomain rule, DevicePingDomain devicePing)
        {
            try
            {
                if (!_ruleEvaluator.Evaluate(rule))
                {
                    CreateIncident(rule, devicePing);
                }
            }
            catch (Exception)
            {
                CreateDeviceNotWorkingIncident(rule, devicePing);
            }
        }

        private void CreateIncident(RuleDomain rule, DevicePingDomain devicePing)
        {
            _incidentRepository.AddIncident(new POSTIncidentDomain()
            {
                TenantId = rule.TenantId,
                IncidentStatus = 1, // 1 indicates a new incident
                DeviceId = devicePing.DeviceId,
                Priority = 2, // low priority by default
                IncidentType = FindOrCreateIncidentType(rule),
                AssigneeId = 7, // who to assign if multiple users belong to the same tenant?
                ReporterId = 8, // who's the reporter if the issue was detected by the rule engine?
            }, rule.TenantId);
        }

        private void CreateDeviceNotWorkingIncident(RuleDomain rule, DevicePingDomain devicePing)
        {
            _incidentRepository.AddIncident(new POSTIncidentDomain()
            {
                TenantId = rule.TenantId,
                IncidentStatus = 1, // 1 indicates a new incident
                DeviceId = devicePing.DeviceId,
                Priority =  4, // high priority if device is not working
                IncidentType = 5, // device not working
                AssigneeId = 7, // who to assign if multiple users belong to the same tenant?
                ReporterId = 7, // who's the reporter if the issue was detected by the rule engine?
            }, rule.TenantId);
        }

        private int FindOrCreateIncidentType(RuleDomain rule)
        {
            var types = _incidentTypeRepository
                .GetAllIncidentTypes()
                .Where(x => x.Name == rule.Name);

            if (types.Any())
            {
                return types.First().IncidentTypeId;
            }

            return _incidentTypeRepository.AddIncidentType(new IncidentTypeDomain()
            {
                TenantId = rule.TenantId,
                Name = rule.Name,
                Code = "1", // all existing types had this code
                IsActive = true
            });
        }
    }
}
