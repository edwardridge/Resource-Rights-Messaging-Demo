using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

namespace Billing
{
    using System;
    using System.Collections.Generic;
    using Contracts.Messages;
    using RepertoireRights.Messages;

    public class DealUpdatedHandler :
        IHandleMessages<DealUpdated>
    {
        static ILog log = LogManager.GetLogger<DealUpdatedHandler>();

        public Task Handle(DealUpdated message, IMessageHandlerContext context)
        {
            log.Info($"Received DealUpdated, Deal code = {message.DealCode}");

            var projectsLinkedToDeal = new List<string>() {"P1", "P2"};

            var sendContractToGrs = new SendContractToGRS()
            {
                DealCode = message.DealCode,
                Projects = projectsLinkedToDeal
            };
            context.Send(sendContractToGrs).ConfigureAwait(false);

            for (var i = 1; i < 4; i++)
            {
                context.Send(new CalculateResourceRightsFromLinking()
                {
                    CalculationId = Guid.NewGuid().ToString(),
                    ISRC = $"ISRC {i}",
                    DealCodes = new List<string>() { message.DealCode, "2" }
                }).ConfigureAwait(false);
            }

            return Task.CompletedTask;
        }
    }
}