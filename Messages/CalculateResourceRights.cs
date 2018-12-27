namespace Messages
{
    using System.Collections.Generic;
    using NServiceBus;

    public class CalculateResourceRights :
        ICommand
    {
        public string ISRC { get; set; }

        public List<RepertoireRightsContractualInfo> DealInfo { get; set; }
    }
}