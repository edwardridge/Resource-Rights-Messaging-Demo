using System.Threading.Tasks;
using NServiceBus;

namespace Billing
{
    using System;
    using System.Collections.Generic;
    using Linking.Messages;
    using RepertoireRights.Messages;

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