namespace RepertoireRights
{
    using System.Threading.Tasks;
    using Contracts.Messages;
    using NServiceBus;
    using NServiceBus.Logging;
    using RepertoireRights.Messages;

    public class CalculateResourceRightsSaga : Saga<CalculateResourceRightsSagaData>,
        IAmStartedByMessages<StartCalculatingResourceRights>,
        IHandleMessages<RepertoireRightsContractualInfoForDealCodesMessage>
    {
        static ILog log = LogManager.GetLogger<CalculateResourceRightsSaga>();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<CalculateResourceRightsSagaData> mapper)
        {
            mapper.ConfigureMapping<StartCalculatingResourceRights>(message => message.CalculationId)
                .ToSaga(sagaData => sagaData.CalculationId);
            mapper.ConfigureMapping<RepertoireRightsContractualInfoForDealCodesMessage>(message => message.CalculationId)
                .ToSaga(sagaData => sagaData.CalculationId);
        }

        public Task Handle(StartCalculatingResourceRights message, IMessageHandlerContext context)
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
                Timestamp = Data.Timestamp,
                DealInfo = message.DealInfo
            }).ConfigureAwait(false);

            MarkAsComplete();

            return Task.CompletedTask;
        }
    }
}
