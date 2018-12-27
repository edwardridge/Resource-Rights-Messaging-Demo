namespace RepertoireRights.Messages
{
    using System;
    using System.Collections.Generic;
    using NServiceBus;

    public class StartCalculatingResourceRights : ICommand
    {
        public string CalculationId { get; set; }

        public string ISRC { get; set; }

        public List<string> DealCodes { get; set; }

        public DateTime Timestamp { get; set; }
    }
}