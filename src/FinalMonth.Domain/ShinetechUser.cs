using Microsoft.AspNetCore.Identity;

namespace FinalMonth.Domain
{
    public class ShinetechUser : IdentityUser
    {
        public bool HowAbout { get; set; }

        public static ShinetechUser Create(string username, string email)
        {
            return new ShinetechUser
            {
                UserName = username,
                Email = email,
                EmailConfirmed = true
            };
        }
    }
}
