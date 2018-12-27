using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Billing
{
    using System;
    using System.Collections.Generic;

    public class ResourceDealLinkAddedHandler : IHandleMessages<ResourceDealLinkAdded>
    {
        public Task Handle(ResourceDealLinkAdded message, IMessageHandlerContext context)
        {
            return context.Send(new CalculateResourceRightsFromLinking() {
                CalculationId = Guid.NewGuid().ToString(),
                ISRC = message.ISRC,
                DealCodes = new List<string>() { message.DealCode, "2" }
            });
        }
    }
}