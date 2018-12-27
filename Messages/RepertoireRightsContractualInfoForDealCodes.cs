namespace Messages
{
    using System.Collections.Generic;
    using NServiceBus;

    public class RepertoireRightsContractualInfoForDealCodes : IMessage
    {
        public string CalculationId { get; set; }

        public List<RepertoireRightsContractualInfo> DealInfo { get; set; }
    }
}