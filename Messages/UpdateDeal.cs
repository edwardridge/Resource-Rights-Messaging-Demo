namespace Messages
{
    using NServiceBus;

    public class UpdateDeal : ICommand
    {
        public string DealCode { get; set; }
    }
}