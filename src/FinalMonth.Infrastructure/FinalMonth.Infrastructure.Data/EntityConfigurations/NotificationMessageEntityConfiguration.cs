using FinalMonth.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalMonth.Infrastructure.Data.EntityConfigurations
{
    public class NotificationMessageEntityConfiguration : IEntityTypeConfiguration<NotificationMessage>
    {
        public void Configure(EntityTypeBuilder<NotificationMessage> builder)
        {
            builder.HasKey(x => x.NotificationId);
            builder.Property(x => x.NotificationId).HasDefaultValueSql("newid()");
            builder.Property(x => x.Message).HasMaxLength(512).IsRequired();
            builder.Property(x => x.From).HasMaxLength(50).IsRequired();
        }
    }
}
