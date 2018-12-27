using System.Threading.Tasks;
using NServiceBus;

namespace Billing
{
    using System;
    using System.Collections.Generic;
    using Contracts.Messages;
    using Linking.Messages;
    using RepertoireRights.Messages;

    public class ProjectDealLinkAddedHandler : IHandleMessages<ProjectDealLinkAdded>
    {
        public Task Handle(ProjectDealLinkAdded message, IMessageHandlerContext context)
        {
            for (var i = 1; i < 4; i++){
                context.Send(new CalculateResourceRightsFromLinking()
                {
                    CalculationId = Guid.NewGuid().ToString(),
                    ISRC = $"ISRC {i}",
                    DealCodes = new List<string>() { message.DealCode, "2" }
                }).ConfigureAwait(false);
            }

            context.Send(new SendContractToGRS() {
                DealCode = message.DealCode,
                Projects = new List<string>() { "1", "2" }
            }).ConfigureAwait(false);

            return Task.CompletedTask;
        }
    }
}