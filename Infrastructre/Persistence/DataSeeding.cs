using DomainLayer.Contracts;
using DomainLayer.Models.Identity;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System.Text.Json;


namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext
                            , StoreIdentityContext _identityContext
                            , RoleManager<IdentityRole> _roleManager
                            , UserManager<User> _userManager) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {

            try
            {
                var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (PendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandData = File.OpenRead(@"..\Infrastructre\Persistence\Data\DataSeed\brands.json");
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands != null && ProductBrands.Any())
                        _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.OpenRead(@"..\Infrastructre\Persistence\Data\DataSeed\types.json");
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);
                    if (ProductTypes != null && ProductTypes.Any())
                        _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                }

                if (!_dbContext.Products.Any())
                {
                    var ProductData = File.OpenRead(@"..\Infrastructre\Persistence\Data\DataSeed\products.json");
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    if (Products != null && Products.Any())
                        _dbContext.Products.AddRangeAsync(Products);
                }

                if (!_dbContext.DeliveryMethods.Any())
                {
                    var DeliveryMethodData = File.OpenRead(@"..\Infrastructre\Persistence\Data\DataSeed\delivery.json");
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodData);
                    if (DeliveryMethods != null && DeliveryMethods.Any())
                        _dbContext.DeliveryMethods.AddRangeAsync(DeliveryMethods);
                }

                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }
        }

        public async Task InitializeIdentityAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!_userManager.Users.Any())
            {
                var superAdmin = new User()
                {
                    DisplayName = "Super Admin",
                    Email = "superadmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "01000000000",
                };
                var admin = new User()
                {
                    DisplayName = "Admin",
                    Email = "admin@gmail.com",
                    UserName = "AdminButNotSuper",
                    PhoneNumber = "0123456789",
                };

                await _userManager.CreateAsync(superAdmin, "Super123*");
                await _userManager.CreateAsync(admin, "Admin123*");

                await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                await _userManager.AddToRoleAsync(admin, "Admin");
            }

            await _identityContext.SaveChangesAsync();
        }
    }
}
