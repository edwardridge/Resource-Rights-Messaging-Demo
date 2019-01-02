namespace Linking
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.Messages;
    using Linking.Messages;
    using NServiceBus;
    using NServiceBus.Logging;
    using RepertoireRights.Messages;

    public class ProjectDealLinkAddedHandler : IHandleMessages<ProjectDealLinkAdded>
    {
        static ILog log = LogManager.GetLogger<ProjectDealLinkAddedHandler>();

        public Task Handle(ProjectDealLinkAdded message, IMessageHandlerContext context)
        {
            log.Info("ProjectDealLinkAdded message received");

            foreach (var isrc in GetIsrcsLinkedToDeal())
            {
                var dealCodes = GetDealsLinkedToResource();

                context.Send(new StartCalculatingResourceRights()
                {
                    CalculationId = Guid.NewGuid().ToString(),
                    ISRC = $"ISRC {isrc}",
                    Timestamp = message.Timestamp,
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