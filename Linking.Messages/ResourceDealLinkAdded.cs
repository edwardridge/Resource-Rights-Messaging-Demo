namespace Linking.Messages
{
    using System;
    using NServiceBus;

    public class ResourceDealLinkAdded : IEvent
    {
        public ResourceDealLinkAdded()
        {
            Timestamp = DateTime.Now;
        }

        public string ISRC { get; set; }

        public string DealCode { get; set; }

        public DateTime Timestamp { get; }
    }
}