using System.Threading.Tasks;

using Azure.Messaging.ServiceBus;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NextEnergy.Ingesters.Imbalance.Clients.Tennet.DependencyInjection;
using NextEnergy.Ingesters.Imbalance.Services.Imbalances;

namespace NextEnergy.Ingesters.Imbalance;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        return await Host
            .CreateDefaultBuilder()
            .ConfigureLogging(context =>
            {
                context.AddConsole(); // Add proper logging here so it is persisted
            })
            .ConfigureServices((context, services) =>
            {
                services
                    .AddTennetClient(context.Configuration.GetValue<string>("clients:tennet:baseaddress")!); 

                services
                    .AddSingleton(_ => new ServiceBusClient(context.Configuration.GetValue<string>("clients:servicebus:connectionstring"))) 
                    .AddSingleton(sp => sp.GetRequiredService<ServiceBusClient>().CreateSender(context.Configuration.GetValue<string>("clients:servicebus:queuename")));

                services
                    .AddTransient<IImbalanceService, ImbalanceService>();

                // TODO: Add PostgreSQL storage
            })
            .RunCommandLineApplicationAsync<Program>(args);
    }    
}
