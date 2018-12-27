namespace RepertoireRights
{
    using System.Collections.Generic;
    using NServiceBus;

    public class CalculateResourceRightsFromLinkingData : ContainSagaData
    {
        public string CalculationId { get; set; }
        public string ISRC { get; set; }
        public List<string> DealCodes { get; set; }
    }
}
