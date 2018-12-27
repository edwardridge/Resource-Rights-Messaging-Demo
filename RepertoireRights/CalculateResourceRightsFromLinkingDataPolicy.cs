namespace RepertoireRights
{
    using System.Threading.Tasks;
    using Messages;
    using NServiceBus;
    using NServiceBus.Logging;

    public class CalculateResourceRightsFromLinkingDataPolicy : Saga<CalculateResourceRightsFromLinkingData>,
        IAmStartedByMessages<CalculateResourceRightsFromLinking>,
        IHandleMessages<RepertoireRightsContractualInfoForDealCodes>
    {
        static ILog log = LogManager.GetLogger<CalculateResourceRightsFromLinkingDataPolicy>();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<CalculateResourceRightsFromLinkingData> mapper)
        {
            mapper.ConfigureMapping<CalculateResourceRightsFromLinking>(message => message.CalculationId)
                .ToSaga(sagaData => sagaData.CalculationId);
            mapper.ConfigureMapping<RepertoireRightsContractualInfoForDealCodes>(message => message.CalculationId)
                .ToSaga(sagaData => sagaData.CalculationId);
        }

        public Task Handle(CalculateResourceRightsFromLinking message, IMessageHandlerContext context)
        {
            log.Info("OrderPlaced message received.");
            Data.ISRC = message.ISRC;

            context.Send(new GetRepertoireRightsContractualInfoForDealCodes()
            {
                CalculationId = Data.CalculationId,
                DealCodes = Data.DealCodes
            }).ConfigureAwait(false);

            return Task.CompletedTask;
        }

        public Task Handle(RepertoireRightsContractualInfoForDealCodes message, IMessageHandlerContext context)
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
