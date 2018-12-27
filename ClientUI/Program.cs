using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

namespace ClientUI
{
    using System.Collections.Generic;
    using Contracts.Messages;
    using Contracts.Messages.Domain;
    using Linking.Messages;
    using RepertoireRights.Messages;

    class Program
    {
        static async Task Main()
        {
            Console.Title = "SagaDemo";

            var endpointConfiguration = new EndpointConfiguration("SagaDemo");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.SendFailedMessagesTo("error");

            var transport = endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.EnableInstallers();
            var persistence = endpointConfiguration.UsePersistence<InMemoryPersistence>();

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(SendContractToGRS), "Contracts");
            routing.RouteToEndpoint(typeof(UpdateDeal), "Contracts");
            routing.RouteToEndpoint(typeof(CalculateResourceRights), "RepertoireRights");
            routing.RouteToEndpoint(typeof(StartCalculatingResourceRights), "RepertoireRights");
            
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            await RunLoop(endpointInstance)
                .ConfigureAwait(false);

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }

        static ILog log = LogManager.GetLogger<Program>();

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                log.Info("Press 'S' to send contract to GRS, 'U' to update a deal, 'C' to calculate resource rights, 'L' to Calculate Resource Rights From Linking Data, 'R' to add resource deal link, 'P' to add project deal link or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.S:
                        await SendContractToGrs(endpointInstance);

                        break;

                    case ConsoleKey.U:
                        await UpdateDeal(endpointInstance);

                        break;

                    case ConsoleKey.C:
                        await CalculateResourceRights(endpointInstance);

                        break;

                    case ConsoleKey.L:
                        await CalculateResourceRightsFromLinking(endpointInstance);

                        break;

                    case ConsoleKey.R:
                        await ResourceDealLinkAdded(endpointInstance);

                        break;

                    case ConsoleKey.P:
                        await ProjectDealLinkAdded(endpointInstance);

                        break;

                    case ConsoleKey.Q:
                        return;

                    default:
                        log.Info("Unknown input. Please try again.");
                        break;
                }
            }
        }

        static async Task ProjectDealLinkAdded(IEndpointInstance endpointInstance)
        {
            var projectDealLinkAdded = new ProjectDealLinkAdded()
            {
                DealCode = "1",
                ProjectCode = "P1"
            };

            log.Info($"Adding project deal link, Deal code = {projectDealLinkAdded.DealCode} Project code = {projectDealLinkAdded.ProjectCode}");
            await endpointInstance.Publish(projectDealLinkAdded)
                .ConfigureAwait(false);
        }

        static async Task ResourceDealLinkAdded(IEndpointInstance endpointInstance)
        {
            var resourceDealLinkAdded = new ResourceDealLinkAdded()
            {
                DealCode = "1",
                ISRC = "ISRC1"
            };

            log.Info($"Adding resource deal link, Deal code = {resourceDealLinkAdded.DealCode} ISRC = {resourceDealLinkAdded.ISRC}");
            await endpointInstance.Publish(resourceDealLinkAdded)
                .ConfigureAwait(false);
        }

        static async Task CalculateResourceRightsFromLinking(IEndpointInstance endpointInstance)
        {
            var calculateResourceRightsFromLinking = new StartCalculatingResourceRights()
            {
                CalculationId = Guid.NewGuid().ToString(),
                ISRC = "ISRC 1",
                DealCodes = new List<string>() {"1", "2"}
            };

            log.Info($"Sending calculate resource rights from linking command, ISRC = {calculateResourceRightsFromLinking.ISRC}");
            await endpointInstance.Send(calculateResourceRightsFromLinking)
                .ConfigureAwait(false);
        }

        static async Task CalculateResourceRights(IEndpointInstance endpointInstance)
        {
            var calculateResourceRights = new CalculateResourceRights()
            {
                ISRC = "ISRC 1",
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
                                RightGranted = true
                            }
                        }
                    },
                    new RepertoireRightsContractualInfo()
                    {
                        DealCode = "2",
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

            log.Info($"Sending calculate resource rights command, ISRC = {calculateResourceRights.ISRC}");
            await endpointInstance.Send(calculateResourceRights)
                .ConfigureAwait(false);
        }

        static async Task UpdateDeal(IEndpointInstance endpointInstance)
        {
            var updateDeal = new UpdateDeal()
            {
                DealCode = "1"
            };

            log.Info($"Sending UpdateDeal command, Deal code = {updateDeal.DealCode}");
            await endpointInstance.Send(updateDeal)
                .ConfigureAwait(false);
        }

        static async Task SendContractToGrs(IEndpointInstance endpointInstance)
        {
            var command = new SendContractToGRS
            {
                DealCode = "1",
                Projects = new List<string>() {"P1", "P2"}
            };

            log.Info($"Sending SendContractToGRS command, Deal code = {command.DealCode}");
            await endpointInstance.Send(command)
                .ConfigureAwait(false);
        }
    }
}