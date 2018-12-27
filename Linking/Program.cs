using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Billing
{
    using Contracts.Messages;
    using Linking.Messages;
    using RepertoireRights.Messages;

    class Program
    {
        static async Task Main()
        {
            Console.Title = "Linking";

            var endpointConfiguration = new EndpointConfiguration("Linking");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl", TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(30));

            var transport = endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.EnableInstallers();
            var persistence = endpointConfiguration.UsePersistence<InMemoryPersistence>();

            var routing = transport.Routing();
            routing.RegisterPublisher(
                assembly: typeof(DealUpdated).Assembly,
                publisherEndpoint: "Contracts");

            routing.RegisterPublisher(
                assembly: typeof(ResourceDealLinkAdded).Assembly,
                publisherEndpoint: "SagaDemo");

            routing.RegisterPublisher(
                assembly: typeof(ProjectDealLinkAdded).Assembly,
                publisherEndpoint: "SagaDemo");

            routing.RouteToEndpoint(typeof(SendContractToGRS), "Contracts");
            routing.RouteToEndpoint(typeof(CalculateResourceRightsFromLinking), "RepertoireRights");
            
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}