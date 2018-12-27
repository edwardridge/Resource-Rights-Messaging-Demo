namespace Messages
{
    using System.Collections.Generic;
    using NServiceBus;

    public class SendContractToGRS : ICommand
    {
        public List<string> Projects { get; set; }

        public string DealCode { get; set; }
    }
}