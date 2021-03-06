using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FinalMonth.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace FinalMonth.Infrastructure.Data
{
    public class FinalMonthDataContext : IdentityDbContext<ShinetechUser>, IFinalMonthDataContext
    {
        public FinalMonthDataContext(DbContextOptions<FinalMonthDataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            builder.ApplyConfiguration(new MemberEntityConfiguration());
            builder.ApplyConfiguration(new NotificationMessageEntityConfiguration());
            base.OnModelCreating(builder);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DatabaseFacade Database => base.Database;

        public DbSet<Member> Members { get; set; }
        public DbSet<NotificationMessage> NotificationMessages { get; set; }
    }
}
