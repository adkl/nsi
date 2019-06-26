using MassTransit;
using NSI.Queue.Messages.Events;
using System;
using System.Threading.Tasks;

namespace NSI.Queue.MessageConsumers
{
    public class LogMessageReceivedConsumer : IConsumer<ILogMessageReceived>
    {
        /// <summary>
        /// Method provides an example how to implement message consumer.
        /// This should be removed later.
        /// </summary>
        /// <param name="context">Message context</param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<ILogMessageReceived> context)
        {
            System.Diagnostics.Debug.WriteLine($"Message received: {context.Message.Message}");
        }
    }
}