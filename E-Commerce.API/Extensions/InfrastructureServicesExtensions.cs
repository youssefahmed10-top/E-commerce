using System.Text;
using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presistence.Data;
using Presistence.Identity;
using Presistence.Repositories;
using Shared.Common;

namespace E_Commerce.API.Extensions
{
    public static class InfrastructureServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<IdentityStoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<IdentityStoreDbContext>();

            //.AddDefaultTokenProviders();   To Create Token ByDefault But not Strong Like JWT

            //services.AddIdentityCore<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<IdentityStoreDbContext>();     

            services.AddScoped<IDataSeed, DataSeed>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.ValidateJwt(configuration);
            return services;

        }
        
        public static IServiceCollection ValidateJwt(this IServiceCollection services , IConfiguration configuration) 
        {
           var jwtoptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtoptions.Issuer,
                    ValidAudience = jwtoptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.SecuretyKey))

                };
            
            });
            services.AddAuthorization();
            return services;
        }
    }
}
