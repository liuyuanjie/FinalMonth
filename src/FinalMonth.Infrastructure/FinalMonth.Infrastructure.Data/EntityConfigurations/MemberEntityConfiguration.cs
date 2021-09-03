using FinalMonth.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalMonth.Infrastructure.Data.EntityConfigurations
{
    public class MemberEntityConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(x => x.MemberId);
            builder.Property(x => x.MemberId).HasDefaultValueSql("newid()");
            builder.Property(x => x.JoinDate).HasDefaultValueSql("getutcdate()");
            builder.Property(x => x.JobTitle).HasMaxLength(50);
            builder.HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<Member>(x => x.UserId);
        }
    }
}
