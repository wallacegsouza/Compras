using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Compras.Messaging;

namespace Compras.Extension
{
    public static class ComprasBrokerConsumer
    {
        public static void UseConsumerBrokerCompraCreate(this IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var consumer = serviceScope.ServiceProvider.GetService<Consumer>();
                consumer.Start();
            }
        }
    }
}