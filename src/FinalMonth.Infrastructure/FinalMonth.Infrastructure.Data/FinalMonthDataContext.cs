using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalMonth.Infrastructure.Data
{
    public class FinalMonthDataContext : IdentityDbContext<ShinetechUser>
    {
        public FinalMonthDataContext(DbContextOptions<FinalMonthDataContext> options)
            : base(options)
        {

        }
    }
}
