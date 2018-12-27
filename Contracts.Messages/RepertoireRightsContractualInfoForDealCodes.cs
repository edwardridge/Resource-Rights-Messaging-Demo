namespace Contracts.Messages
{
    using System.Collections.Generic;
    using Contracts.Messages.Domain;
    using NServiceBus;

    public class RepertoireRightsContractualInfoForDealCodesMessage : IMessage
    {
        public string CalculationId { get; set; }

        public List<RepertoireRightsContractualInfo> DealInfo { get; set; }
    }
}