using System.Threading;
using System.Threading.Tasks;

using Azure.Messaging.ServiceBus;

using NextEnergy.Ingesters.Imbalance.Clients.Tennet;
using NextEnergy.Ingesters.Imbalance.Repositories;

namespace NextEnergy.Ingesters.Imbalance.Services.Imbalances
{
    public interface IImbalanceService
    {
        Task IngestImbalancesAsync(CancellationToken cancellationToken);
    }

    internal sealed class ImbalanceService : IImbalanceService
    {
        private readonly ITennetClient client;
        private readonly IImbalanceRepository repository;
        private readonly ServiceBusSender sender;

        public ImbalanceService(ITennetClient client, IImbalanceRepository repository, ServiceBusSender sender)
        {
            this.client = client;
            this.repository = repository;
            this.sender = sender;
        }

        public Task IngestImbalancesAsync(CancellationToken cancellationToken)
        {
            // Retrieve imbalances
            // Retrieve latest imbalance from DB
            // Filter out imbalances already added & add the once missing
            // Publish message to sender
            return Task.CompletedTask;
        }
    }
}
