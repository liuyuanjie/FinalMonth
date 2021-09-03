using System.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using FinalMonth.Application.Repository;
using FinalMonth.Domain;
using FinalMonth.Infrastructure.Data.EntityConfigurations;

namespace FinalMonth.Infrastructure.Data
{
    public class FinalMonthDbContext : IdentityDbContext<ShinetechUser>, IFinalMonthDBContext
    {
        public FinalMonthDbContext(DbContextOptions<FinalMonthDbContext> options)
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

        public IDbConnection DbConnection => base.Database.GetDbConnection();

        public DbSet<Member> Members { get; set; }
        public DbSet<NotificationMessage> NotificationMessages { get; set; }
    }
}
