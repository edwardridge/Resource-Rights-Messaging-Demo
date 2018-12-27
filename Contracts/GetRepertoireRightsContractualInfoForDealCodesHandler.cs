namespace Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.Messages;
    using Contracts.Messages.Domain;
    using NServiceBus;
    using NServiceBus.Logging;

    public class GetRepertoireRightsContractualInfoForDealCodesHandler :
        IHandleMessages<GetRepertoireRightsContractualInfoForDealCodes>
    {
        static ILog log = LogManager.GetLogger<GetRepertoireRightsContractualInfoForDealCodesHandler>();

        public Task Handle(GetRepertoireRightsContractualInfoForDealCodes message, IMessageHandlerContext context)
        {
            log.Info($"Received GetRepertoireRightsContractualInfoForDealCodes,Calculation id = {message.CalculationId}");

            var info = new RepertoireRightsContractualInfoForDealCodesMessage()
            {
                CalculationId = message.CalculationId,
                DealInfo = new List<RepertoireRightsContractualInfo>()
                {
                    new RepertoireRightsContractualInfo()
                    {
                        DealCode = "1",
                        Rights = new List<Rights>()
                        {
                            new Rights()
                            {
                                Name = "Digital",
                                RightGranted = false
                            }
                        }
                    }
                }
            };

            return context.Reply(info);
        }
    }
}