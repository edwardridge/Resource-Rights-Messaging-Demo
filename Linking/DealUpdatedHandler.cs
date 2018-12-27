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
            SendContractToGrs();

            foreach (var isrc in GetIsrcsLinkedToDeal())
            {
                SendCalculateResourceRightsCommand(isrc);
            }

            return Task.CompletedTask;

            void SendContractToGrs()
            {
                var sendContractToGrs = new SendContractToGRS()
                {
                    DealCode = message.DealCode,
                    Projects = projectsLinkedToDeal
                };
                context.Send(sendContractToGrs).ConfigureAwait(false);
            }
            void SendCalculateResourceRightsCommand(string isrc)
            {
                context.Send(new StartCalculatingResourceRights()
                {
                    CalculationId = Guid.NewGuid().ToString(),
                    ISRC = $"ISRC {isrc}",
                    Timestamp = message.Timestamp,
                    DealCodes = new List<string>() {message.DealCode, "2"}
                }).ConfigureAwait(false);
            }
        }

        public List<string> GetIsrcsLinkedToDeal () => new List<string>() { "1", "2" };
    }
}