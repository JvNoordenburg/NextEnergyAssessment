using System.Threading;
using System.Threading.Tasks;

using McMaster.Extensions.CommandLineUtils;
using NextEnergy.Ingesters.Imbalance.Services.Imbalances;

namespace NextEnergy.Ingesters.Imbalance.Commands;

[Command(Name = "ingest", Description = "Ingest Imbalance data")]
public class IngestImbalancesCommand
{
    private readonly IImbalanceService imbalanceService;

    public IngestImbalancesCommand(IImbalanceService imbalanceService)
    {
        this.imbalanceService = imbalanceService;
    }

    private async Task OnExecuteAsync(CancellationToken cancellationToken)
    {
        await imbalanceService.IngestImbalancesAsync(cancellationToken);
    }
}
