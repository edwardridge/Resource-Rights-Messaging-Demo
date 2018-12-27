namespace RepertoireRights
{
    using System;
    using System.Threading.Tasks;
    using Messages;
    using NServiceBus;

    class Program
    {
        static async Task Main()
        {
            Console.Title = "RepertoireRights";

            var endpointConfiguration = new EndpointConfiguration("RepertoireRights");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.SendFailedMessagesTo("error");

            var transport = endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.EnableInstallers();
            var persistence = endpointConfiguration.UsePersistence<InMemoryPersistence>();

            var routing = transport.Routing();
            routing.RegisterPublisher(
                assembly: typeof(RepertoireRightsContractualInfoForDealCodes).Assembly,
                publisherEndpoint: "Contracts");

            routing.RouteToEndpoint(typeof(GetRepertoireRightsContractualInfoForDealCodes), "Contracts");

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}