using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


using Compras.Seed;
using Compras.Data;
using Compras.Services;
using Compras.Extension;
using Compras.Messaging;
using Compras.Repositories;

namespace Compras
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(); 
            if (Env.IsEnvironment("Docker"))
            {
                var connectionString = Configuration["mysqlconnection:connectionString"];
                services.AddDbContext<MysqlDataContext>(opt => opt.UseMySQL(connectionString));
                services.AddScoped<DataContext, MysqlDataContext>();
            }
            else if (Env.IsDevelopment())
            {
                services.AddDbContext<InMemoryDataContext>(opt => opt.UseInMemoryDatabase("Database"));
                services.AddScoped<DataContext, InMemoryDataContext>();
            }
            
            services.AddScoped<ConnectionBroker, ConnectionBroker>();
            services.AddScoped<Producer, Producer>();
            services.AddScoped<Consumer, Consumer>();
            services.AddScoped<TokenService, TokenService>();
            services.AddScoped<ClienteRepository, ClienteRepository>();
            services.AddScoped<CompraRepository, CompraRepository>();

            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("Secret"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSeedDb(true);
            }

            if (!env.IsEnvironment("Docker"))
            {
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseSeedQueue();
                app.UseConsumerBrokerCompraCreate();
            }

            app.UseRouting();
            app.UseCors( x => x
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
