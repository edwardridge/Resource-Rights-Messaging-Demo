namespace RepertoireRights
{
    using NServiceBus;

    public class CalculateResourceRightsFromLinkingData : ContainSagaData
    {
        public string CalculationId { get; set; }

        public string ISRC { get; set; }
    }
}
