using Microsoft.AspNetCore.Identity;

namespace PharmaEase.Models.Seeders
{
    public static class SeedData
    {

        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            //make an admin and give him an id
            var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@pharmaease.com");
            await EnsureRole(serviceProvider, adminID, "Admin");

            MedicationSeeder.Initialize(serviceProvider);
            PrescriptionSeeder.Initialize(serviceProvider, adminID);
            //put rest of seeders here
            CourierSeeder.Initialize(serviceProvider);
        }


        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<Patient>>();

            if (userManager == null)
            {
               throw new Exception("userManager is null");
            }

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }


    }
}
