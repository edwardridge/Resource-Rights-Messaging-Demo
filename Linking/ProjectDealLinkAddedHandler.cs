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
            foreach (var isrc in GetIsrcsLinkedToDeal())
            {
                var dealCodes = GetDealsLinkedToResource();

                context.Send(new CalculateResourceRightsFromLinking()
                {
                    CalculationId = Guid.NewGuid().ToString(),
                    ISRC = $"ISRC {isrc}",
                    DealCodes = dealCodes
                }).ConfigureAwait(false);
            }

            var projectsLinkedToDeal = new List<string>() { "P1", "P2" };
            SendContractToGrs();

            return Task.CompletedTask;

            void SendContractToGrs()
            {
                context.Send(new SendContractToGRS()
                {
                    DealCode = message.DealCode,
                    Projects = projectsLinkedToDeal
                }).ConfigureAwait(false);
            }
        }

        public List<string> GetDealsLinkedToResource() => new List<string>() {"1", "2"};

        public List<string> GetIsrcsLinkedToDeal() => new List<string>() {"1", "2"};
    }
}