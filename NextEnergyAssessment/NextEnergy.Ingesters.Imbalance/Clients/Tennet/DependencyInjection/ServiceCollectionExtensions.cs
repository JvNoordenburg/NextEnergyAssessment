using System;

using Microsoft.Extensions.DependencyInjection;

namespace NextEnergy.Ingesters.Imbalance.Clients.Tennet.DependencyInjection
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddTennetClient(this IServiceCollection services, string baseAddress)
        {
            services
                .AddHttpClient<TennetClient>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(baseAddress);
                });

            services
                .AddTransient<ITennetClient, TennetClient>();

            return services;
        }
    }
}
