using System.Threading;
using System.Threading.Tasks;

using NextEnergy.Ingesters.Imbalance.Models;

namespace NextEnergy.Ingesters.Imbalance.Repositories
{
    internal interface IImbalanceRepository
    {
        public Task AddImbalanceEntryAsync(ImbalanceEntry entry, CancellationToken cancellationToken);
        public Task<ImbalanceEntry?> GetLatestImbalanceEntry(CancellationToken cancellationToken);
    }    
}
