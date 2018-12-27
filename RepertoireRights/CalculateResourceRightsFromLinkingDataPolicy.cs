namespace RepertoireRights
{
    using System.Threading.Tasks;
    using Contracts.Messages;
    using NServiceBus;
    using NServiceBus.Logging;
    using RepertoireRights.Messages;

    public class CalculateResourceRightsFromLinkingDataPolicy : Saga<CalculateResourceRightsFromLinkingData>,
        IAmStartedByMessages<CalculateResourceRightsFromLinking>,
        IHandleMessages<RepertoireRightsContractualInfoForDealCodesMessage>
    {
        static ILog log = LogManager.GetLogger<CalculateResourceRightsFromLinkingDataPolicy>();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<CalculateResourceRightsFromLinkingData> mapper)
        {
            mapper.ConfigureMapping<CalculateResourceRightsFromLinking>(message => message.CalculationId)
                .ToSaga(sagaData => sagaData.CalculationId);
            mapper.ConfigureMapping<RepertoireRightsContractualInfoForDealCodesMessage>(message => message.CalculationId)
                .ToSaga(sagaData => sagaData.CalculationId);
        }

        public Task Handle(CalculateResourceRightsFromLinking message, IMessageHandlerContext context)
        {
            log.Info("OrderPlaced message received.");
            Data.ISRC = message.ISRC;
            Data.Timestamp = message.Timestamp;

            context.Send(new GetRepertoireRightsContractualInfoForDealCodes()
            {
                CalculationId = Data.CalculationId,
                DealCodes = message.DealCodes
            }).ConfigureAwait(false);

            return Task.CompletedTask;
        }

        public Task Handle(RepertoireRightsContractualInfoForDealCodesMessage message, IMessageHandlerContext context)
        {
            var handle = context.SendLocal(new CalculateResourceRights()
            {
                ISRC = Data.ISRC,
                DealInfo = message.DealInfo
            }).ConfigureAwait(false);

            MarkAsComplete();

            return Task.CompletedTask;
        }
    }
}
