﻿namespace RepertoireRights.Messages
{
    using System;
    using System.Collections.Generic;
    using Contracts.Messages.Domain;
    using NServiceBus;

    public class CalculateResourceRights :
        ICommand
    {
        public string ISRC { get; set; }

        public List<RepertoireRightsContractualInfo> DealInfo { get; set; }

        public DateTime Timestamp { get; set; }
    }
}