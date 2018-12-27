namespace Linking.Messages
{
    using NServiceBus;

    public class ResourceDealLinkAdded : IEvent
    {
        public string ISRC { get; set; }

        public string DealCode { get; set; }
    }
}