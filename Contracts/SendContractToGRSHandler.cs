namespace Contracts
{
    using System.Threading.Tasks;
    using Messages;
    using NServiceBus;
    using NServiceBus.Logging;

    public class SendContractToGRSHandler :
        IHandleMessages<SendContractToGRS>
    {
        static ILog log = LogManager.GetLogger<SendContractToGRS>();

        public Task Handle(SendContractToGRS message, IMessageHandlerContext context)
        {
            log.Info($"Received SendContractToGRS, Deal code = {message.DealCode}");

            //var orderPlaced = new OrderPlaced
            //{
            //    OrderId = message.OrderId
            //};
            //return context.Publish(orderPlaced);

            return Task.CompletedTask;
        }
    }
}