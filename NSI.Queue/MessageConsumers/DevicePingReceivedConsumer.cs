using MassTransit;
using NSI.Queue.Messages.Events;
using System.Threading.Tasks;
using NSI.RuleEvaluator;

namespace NSI.Queue.MessageConsumers
{
    public class DevicePingReceivedConsumer : IConsumer<IDevicePingReceived>
    {
        /// <summary>
        /// Method provides an example how to implement message consumer.
        /// This should be removed later.
        /// </summary>
        /// <param name="context">Message context</param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<IDevicePingReceived> context)
        {
            DevicePingListener.GetInstance().EvaluateAll(context.Message.DevicePing);
        
            System.Diagnostics.Debug.WriteLine($"Device Ping received: {context.Message.DevicePing}");
        }
    }
}
