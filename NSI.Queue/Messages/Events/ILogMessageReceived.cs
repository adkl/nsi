using System;

namespace NSI.Queue.Messages.Events
{
    public interface ILogMessageReceived
    {
        Guid MessageId { get; }
        DateTime Timestamp { get; }
        string Message { get; }
    }
}
