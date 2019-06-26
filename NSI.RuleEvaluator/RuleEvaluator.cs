using NSI.Domain.DevicePing;
using NSI.Domain.RuleEngine;
using NSI.Repository.Interfaces.DevicePing;
using NSI.RuleEvaluator.Comparator;

namespace NSI.RuleEvaluator
{
    public class RuleEvaluator
    {
        private readonly IDevicePingRepository _devicePingRepository;

        public RuleEvaluator(IDevicePingRepository devicePingRepository)
        {
            _devicePingRepository = devicePingRepository;
        }

        public bool Evaluate(RuleDomain rule)
        {
            ComparatorFactory factory = new ComparatorFactory();

            foreach (var condition in rule.Conditions)
            {
                var latestDevicePing = _devicePingRepository.LastDevicePingForDevice(condition.Device.DeviceId);

                DevicePropertyValue latestValue = latestDevicePing.DevicePropertyValues.Find(
                    x => x.PropertyId == condition.Property.PropertyId
                );

                if (!factory.Make(condition.ComparisonOperator).Compare(latestValue.Value, condition.ComparisonValue))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
