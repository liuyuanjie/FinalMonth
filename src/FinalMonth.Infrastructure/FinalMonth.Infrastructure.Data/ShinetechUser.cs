using Microsoft.AspNetCore.Identity;

namespace FinalMonth.Infrastructure.Data
{
    public class ShinetechUser : IdentityUser
    {
        public bool HowAbout { get; set; }
    }
}
