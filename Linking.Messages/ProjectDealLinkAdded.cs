namespace Linking.Messages
{
    using NServiceBus;

    public class ProjectDealLinkAdded : IEvent
    {
        public string ProjectCode { get; set; }

        public string DealCode { get; set; }
    }
}