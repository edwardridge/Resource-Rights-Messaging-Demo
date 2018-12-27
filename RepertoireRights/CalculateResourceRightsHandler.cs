namespace RepertoireRights
{
    using System.Threading.Tasks;
    using Messages;
    using NServiceBus;
    using NServiceBus.Logging;

    public class CalculateResourceRightsHandler : IHandleMessages<CalculateResourceRights>
    {
        static ILog log = LogManager.GetLogger<CalculateResourceRightsHandler>();

        public Task Handle(CalculateResourceRights message, IMessageHandlerContext context)
        {
            log.Info($"Calculating resource rights for ISRC: [{message.ISRC}].");
            return Task.CompletedTask;
        }
    }
}
