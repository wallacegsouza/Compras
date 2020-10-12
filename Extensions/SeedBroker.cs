using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Compras.Messaging;

namespace Compras.Seed
{
    public static class SeedBrakerExtension
    {
        public static void UseSeedQueue(this IApplicationBuilder app)
        {
            // TODO: adicionar um check no healthcheck do broker (http://localhost:15672/api/healthchecks/node)
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var producer = serviceScope.ServiceProvider.GetService<Producer>();
                producer.CreateQueue();
            }
        }
    }
}