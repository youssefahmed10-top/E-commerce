using System.Text.Json;
using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;

namespace Presistence.Data
{
    public class  DataSeed(StoreDbContext _dbContext
        ,UserManager<User> _userManager
        ,RoleManager<IdentityRole> _roleManager) : IDataSeed
    {
        public async Task SeedIdentityDataAsync()
        {
            try
            {
                //1]Seed Roles {Admin , SuperAdmin}
                if (!_roleManager.Roles.Any()) 
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                //2]Seed Users {Admin , SuperAdmin}
                if (!_userManager.Users.Any()) 
                {
                    var AdminUser = new User()
                    {
                        DisplayName="Admin",
                        UserName = "Admin",
                        Email="Admin@gamil.Com",
                        PhoneNumber = "01098764532"
                    };
                    var SuperAdminUser = new User()
                    {
                        DisplayName = "SuperAdmin",
                        UserName = "SuperAdmin",
                        Email = "SuperAdmin@gamil.Com",
                        PhoneNumber = "01028428451",
                        
                    };

                    await _userManager.CreateAsync(AdminUser, "P@ssw0rd");
                    await _userManager.CreateAsync(SuperAdminUser, "P@ssw00rd");


                    //3]Assign Roles ==> Users
                    await _userManager.AddToRoleAsync(AdminUser, "Admin");
                    await _userManager.AddToRoleAsync(SuperAdminUser, "Admin");
                }

            }
            catch (Exception) 
            {
                throw;
            }
        }

        async Task IDataSeed.DataSeedAsync()
        {

            var Pending = await _dbContext.Database.GetPendingMigrationsAsync();

            try
            {
                if (Pending.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                if (!_dbContext.productBrands.Any())
                {

                    var productBrandData = File.OpenRead("..\\Infrastructure\\Presistence\\Data\\DataSeed\\brands.json");
                    var productBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandData);

                    if (productBrands is not null && productBrands.Any()) 
                    {
                       await _dbContext.productBrands.AddRangeAsync(productBrands);
                    }
                }

                if (!_dbContext.productTypes.Any())
                {
                    var productTypeData = File.OpenRead("..\\Infrastructure\\Presistence\\Data\\DataSeed\\types.json");
                    var productTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypeData);

                    if (productTypes is not null && productTypes.Any())
                    {
                      await _dbContext.productTypes.AddRangeAsync(productTypes);
                    }

                }

                if (!_dbContext.products.Any())
                {
                    var productData = File.OpenRead("..\\Infrastructure\\Presistence\\Data\\DataSeed\\products.json");
                    var products =await JsonSerializer.DeserializeAsync<List<Product>>(productData);

                    if(products is not null && products.Any())
                    {
                       
                        await _dbContext.products.AddRangeAsync(products);
                    }

                }

                 await _dbContext.SaveChangesAsync();   

            }
            catch (Exception ex) 
            {
            
            }
        }
    }
}
