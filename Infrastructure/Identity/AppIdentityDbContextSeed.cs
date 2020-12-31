using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Velin",
                    Email = "velin@abbov.com",
                    UserName = "arbov",
                    Address = new Address
                    {
                        FirstName = "Velin",
                        LastName = "Arbov",
                        Street = "10 Car Boris",
                        City = "Pleven",
                        State = "NY",
                        Zipcode = "5800"

                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }


        }
    }
}