using MassTransit;
using NSI.Queue.Messages.Events;
using System.Threading.Tasks;

namespace NSI.Queue.MessageConsumers
{
    public class PingDeviceReceivedConsumer : IConsumer<IPingDeviceReceived>
    {
        /// <summary>
        /// Method provides an example how to implement message consumer.
        /// This should be removed later.
        /// </summary>
        /// <param name="context">Message context</param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<IPingDeviceReceived> context)
        {
            System.Diagnostics.Debug.WriteLine($"Ping to device received: {context.Message.Content}; Action: {context.Message.ActionId}");
        }
    }
}
