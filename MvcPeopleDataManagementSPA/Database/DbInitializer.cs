using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Database
{
    public class DbInitializer
    {
        public static void Initialize(
            PeopleDbContext context, 
            RoleManager<IdentityRole> roleManager, 
            UserManager<IdentityAppUser> userManager
            )
        {
            //context.Database.EnsureCreated(); //If not using EF migrations
            context.Database.Migrate();

            if (context.Roles.Any())
            {
                return; // found roles
            }

            //--------------------------Seed into database----------------------------//

            string[] rolesToSeed = new string[] { "Admin" };

            foreach (var roleName in rolesToSeed)
            {
                IdentityRole role = new IdentityRole(roleName);

                var result = roleManager.CreateAsync(role).Result;

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create Role: " + roleName);
                }
            }


            IdentityAppUser user = new IdentityAppUser()
            {
                UserName = "Admin",
                FirstName = "Chris",
                LastName = "Chris",
                BirthDate = new DateTime(2000, 1, 1),
                Email = "email@domain.com",
                PhoneNumber = "0046123456789"
            };

            IdentityResult resultUser = userManager.CreateAsync(user, "demoPass-1").Result;

            if (!resultUser.Succeeded)
            {
                throw new Exception("Failed to create Admin account:  + AdminMain");
            }

            IdentityResult resultAssign = userManager.AddToRoleAsync(user, rolesToSeed[0]).Result;

            if (!resultAssign.Succeeded)
            {
                throw new Exception($"Failed to grant {rolesToSeed[0]} role to AdminMain");
            }

        }
    }
}
