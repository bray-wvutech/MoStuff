﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace UI.Utilities;

static public class AdminHelper
{
    public const string ADMIN_POLICY = "AdminPolicy";

    public const string ADMIN_ROLE = "Admin";
    
    public const string ADMIN_EMAIL = "admin@mostuff.com";

    // DANGER! PASSWORD MUST BE:
    // 6+ chars
    // at least one non alphanumerc character
    // at least one digit ('0'-'9')
    // at least one uppercase ('A'-'Z')
    public const string ADMIN_PASSWORD = "Password123!";

    public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
    {
        var roleManager = 
            serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // check if we already have an admin role
        if (!await roleManager.RoleExistsAsync(ADMIN_ROLE))
        {
            // if not make the admin role
            await roleManager.CreateAsync(new IdentityRole(ADMIN_ROLE));
        }

        // now we are going to make a default admin user
        var userManager =
            serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // see if we have already created the user
        // if not create them and give them the admin role
        if (await userManager.FindByEmailAsync(ADMIN_EMAIL) == null)
        {
            var user = new IdentityUser
            {
                UserName = ADMIN_EMAIL,
                Email = ADMIN_EMAIL,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, ADMIN_PASSWORD);
            await userManager.AddToRoleAsync(user, ADMIN_ROLE);
        }
    }
}
