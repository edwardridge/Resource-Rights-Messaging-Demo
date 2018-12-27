namespace RepertoireRights
{
    using System;
    using NServiceBus;

    public class CalculateResourceRightsSagaData : ContainSagaData
    {
        public string CalculationId { get; set; }

        public DateTime Timestamp { get; set; }

        public string ISRC { get; set; }
    }
}
