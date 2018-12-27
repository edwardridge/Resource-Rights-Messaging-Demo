namespace Contracts.Messages.Domain
{
    using System.Collections.Generic;

    public class RepertoireRightsContractualInfo
    {
        public string DealCode { get; set; }

        public List<Rights> Rights { get; set; }
    }
}