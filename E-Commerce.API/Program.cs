using Domain.Contracts;
using E_Commerce.API.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Presistence.Repositories;
using StackExchange.Redis;

namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Di Contuner
            var builder = WebApplication.CreateBuilder(args);

            //WebApi Services
            builder.Services.AddWebApiServices();

            //Infrastructure Services
            builder.Services.AddInfrastructureServices(builder.Configuration);

            //Core Services
            builder.Services.AddCoreServices(builder.Configuration);
            #endregion



            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnction")!);
            });
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();


            #region Pipelines - Middelwares
            var app = builder.Build();

            await app.SeedDatebaseAsync();

            app.UseExceptionHandlingMiddelwares();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddelWares();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync(); 
            #endregion
        }
    }
}
