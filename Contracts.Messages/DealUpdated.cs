namespace Contracts.Messages
{
    using System;
    using NServiceBus;

    public class DealUpdated : IEvent
    {
        public DealUpdated()
        {
            Timestamp = DateTime.Now;
        }

        public string DealCode { get; set; }

        public DateTime Timestamp { get; }
    }
}