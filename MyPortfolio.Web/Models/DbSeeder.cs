using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MyPortfolio.Web.Models.Entities;
using System;
using System.Threading.Tasks;

namespace MyPortfolio.Web.Models
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger("DbSeeder");
            // Gerekli servisleri al
            var userManager = serviceProvider.GetRequiredService<UserManager<Admin>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            // "Admin" rolünü oluştur
            var roleName = "Admin";
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
                logger.LogInformation("'{RoleName}' rolü oluşturuldu.", roleName);
            }

            // "admin" kullanıcısını bul
            var adminUserName = "admin";
            var adminUser = await userManager.FindByNameAsync(adminUserName);
            
            // Eğer kullanıcı yoksa, oluştur.
            if (adminUser == null)
            {
                var newAdminUser = new Admin
                {
                    UserName = adminUserName,
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newAdminUser, "admin123");

                if (result.Succeeded)
                {
                    logger.LogInformation("'{AdminUserName}' kullanıcısı başarıyla oluşturuldu.", adminUserName);
                    // Kullanıcıya "Admin" rolünü ata
                    await userManager.AddToRoleAsync(newAdminUser, roleName);
                    logger.LogInformation("'{AdminUserName}' kullanıcısına '{RoleName}' rolü atandı.", adminUserName, roleName);
                }
                else
                {
                    logger.LogError("'{AdminUserName}' kullanıcısı oluşturulurken hata oluştu.", adminUserName);
                    foreach (var error in result.Errors)
                    {
                        logger.LogError("Hata: {Code} - {Description}", error.Code, error.Description);
                    }
                }
            }
        }
    }
} 