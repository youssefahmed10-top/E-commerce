using Domain.Contracts;
using E_Commerce.API.Middelwares;

namespace E_Commerce.API.Extensions
{
    public static class WebApplicationExtentions
    {
        public static async Task<WebApplication> SeedDatebaseAsync(this WebApplication app) 
        {

            using var Scope = app.Services.CreateScope();
            var ObjectOfDataSeed = Scope.ServiceProvider.GetRequiredService<IDataSeed>();

            await ObjectOfDataSeed.DataSeedAsync();
            await ObjectOfDataSeed.SeedIdentityDataAsync();

            return app;
        }

        public static WebApplication UseExceptionHandlingMiddelwares(this WebApplication app) 
        {
            app.UseMiddleware<GlobalExceptionHandling>();

            return app;
        }

        public static WebApplication UseSwaggerMiddelWares(this WebApplication app) 
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}
