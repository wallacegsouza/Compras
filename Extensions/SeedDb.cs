using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Compras.Data;
using Compras.Models;

namespace Compras.Seed
{
    public static class SeedDbExtension
    {
        public static void UseSeedDb(this IApplicationBuilder app, bool development = false)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var context = serviceScope.ServiceProvider.GetService<DataContext>())
            {
                if(development)
                    context.Seed();
            }
        }

        private static void Seed(this DataContext context)
        {
            context.Add(new Cliente { Id = 100, Email = "teste@gmail.com", Login = "teste", Senha = "123456" });
            context.Add(new Cliente { Id = 101, Email = "teste2@gmail.com", Login = "elvis", Senha = "987654" });
            context.SaveChanges();
        }   
    }
    
}