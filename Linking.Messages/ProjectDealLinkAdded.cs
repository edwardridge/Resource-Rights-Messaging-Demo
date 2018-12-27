namespace Linking.Messages
{
    using System;
    using NServiceBus;

    public class ProjectDealLinkAdded : IEvent
    {
        public ProjectDealLinkAdded()
        {
            this.Timestamp = DateTime.Now;
        }

        public string ProjectCode { get; set; }

        public string DealCode { get; set; }

        public DateTime Timestamp { get; }
    }
}