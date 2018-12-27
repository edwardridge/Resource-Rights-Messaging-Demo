namespace Contracts.Messages
{
    using System.Collections.Generic;
    using NServiceBus;

    public class GetRepertoireRightsContractualInfoForDealCodes : ICommand
    {
        public string CalculationId { get; set; }

        public List<string> DealCodes { get; set; }
    }
}