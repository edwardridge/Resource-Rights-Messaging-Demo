namespace RepertoireRights.Messages
{
    using System.Collections.Generic;
    using NServiceBus;

    public class CalculateResourceRightsFromLinking : ICommand
    {
        public string CalculationId { get; set; }

        public string ISRC { get; set; }

        public List<string> DealCodes { get; set; }
    }
}