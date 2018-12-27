namespace RepertoireRights
{
    using System;
    using NServiceBus;

    public class CalculateResourceRightsFromLinkingData : ContainSagaData
    {
        public string CalculationId { get; set; }

        public DateTime Timestamp { get; set; }

        public string ISRC { get; set; }
    }
}
