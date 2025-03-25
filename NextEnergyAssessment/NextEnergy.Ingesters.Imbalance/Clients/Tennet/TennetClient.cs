using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using NextEnergy.Ingesters.Imbalance.Models;

namespace NextEnergy.Ingesters.Imbalance.Clients.Tennet
{
    internal interface ITennetClient
    {
        Task<List<ImbalanceEntry>> GetImbalancesAsync(CancellationToken cancellationToken);
    }

    internal sealed class TennetClient : ITennetClient
    {
        private readonly HttpClient client;

        private static readonly JsonSerializerOptions serializerOptions = JsonSerializerOptions.Default;

        public TennetClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<ImbalanceEntry>> GetImbalancesAsync(CancellationToken cancellationToken)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "ImbalancePrices");
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage, cancellationToken);

            responseMessage.EnsureSuccessStatusCode();

            return (await JsonSerializer.DeserializeAsync<List<ImbalanceEntry>>(responseMessage.Content.ReadAsStream(cancellationToken), serializerOptions, cancellationToken))!;           
        }
    }
}
