namespace Contracts.Messages
{
    using NServiceBus;

    public class DealUpdated : IEvent
    {
        public string DealCode { get; set; }
    }
}