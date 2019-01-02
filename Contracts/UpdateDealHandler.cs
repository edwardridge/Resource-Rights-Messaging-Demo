﻿namespace Contracts
{
    using System.Threading.Tasks;
    using Contracts.Messages;
    using NServiceBus;
    using NServiceBus.Logging;

    public class UpdateDealHandler :
        IHandleMessages<UpdateDeal>
    {
        static ILog log = LogManager.GetLogger<UpdateDealHandler>();

        public Task Handle(UpdateDeal message, IMessageHandlerContext context)
        {
            log.Info("UpdateDeal message received");

            return context.Publish(new DealUpdated()
            {
                DealCode = message.DealCode
            });
        }
    }
}