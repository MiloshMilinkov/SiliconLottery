using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager){
            if(!userManager.Users.Any()){
                var user=new AppUser{
                    DisplayName="milos",
                    Email="milos@gmail.com",
                    UserName="milos",
                    Address= new Address{
                        StreetName="test street",
                        StreetNumber=1,
                        City="test city",
                        ZipCode="test1 "
                    }
                };

                await userManager.CreateAsync(user,"Pasword1234.");
            }
        }
    }
}