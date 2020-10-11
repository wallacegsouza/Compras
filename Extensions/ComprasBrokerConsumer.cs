using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Compras.Messaging;
using Compras.Repositories;

namespace Compras.Extension
{
    public static class ComprasBrokerConsumer
    {
        public static void UseConsumerBrokerCompraCreate(this IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var consumer = serviceScope.ServiceProvider.GetService<Consumer>();
            var respository = serviceScope.ServiceProvider.GetService<CompraRepository>();
            consumer.Start(respository);
        }
    }
}