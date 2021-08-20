using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;

namespace FinalMonth.Infrastructure.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            var yuanjie = await EnsureUser(serviceProvider, testUserPw, "liuyj@shinetechchina.com", "leader", "developer", "tester");
            await EnsureRole(serviceProvider, yuanjie, "admin");
            await EnsureRole(serviceProvider, yuanjie, "test");
            await EnsureRole(serviceProvider, yuanjie, "develop");

            var lixing = await EnsureUser(serviceProvider, testUserPw, "lixing@shinetechchina.com", "tester", "developer");
            await EnsureRole(serviceProvider, lixing, "test");
            await EnsureRole(serviceProvider, lixing, "develop");

            var managerID = await EnsureUser(serviceProvider, testUserPw, "zhangchao@shinetechchina.com", "developer");
            await EnsureRole(serviceProvider, managerID, "develop");
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string email, params string[] claims)
        {
            var userManager = serviceProvider.GetService<UserManager<ShinetechUser>>();

            var username = email.Split('@')[0];
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                user = new ShinetechUser
                {
                    UserName = username,
                    Email = email,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);

                foreach (var claim in claims)
                {
                    await userManager.AddClaimsAsync(user, new Claim[] { new Claim(ClaimTypes.Actor, claim) });
                }
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
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<ShinetechUser>>();

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
