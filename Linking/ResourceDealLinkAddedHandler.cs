namespace Linking
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Linking.Messages;
    using NServiceBus;
    using NServiceBus.Logging;
    using RepertoireRights.Messages;

    public class ResourceDealLinkAddedHandler : IHandleMessages<ResourceDealLinkAdded>
    {
        static ILog log = LogManager.GetLogger<ResourceDealLinkAddedHandler>();

        public Task Handle(ResourceDealLinkAdded message, IMessageHandlerContext context)
        {
            log.Info("ResourceDealLinkAdded message received");

            return context.Send(new StartCalculatingResourceRights() {
                CalculationId = Guid.NewGuid().ToString(),
                ISRC = message.ISRC,
                Timestamp = message.Timestamp,
                DealCodes = new List<string>() { message.DealCode, "2" }
            });
        }
    }
}