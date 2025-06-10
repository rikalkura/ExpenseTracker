using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Infrastructure.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.Property(e => e.Token)
            .IsRequired();

        builder.Property(e => e.ExpiryDate)
            .IsRequired();

        builder.HasQueryFilter(e => !e.IsUsed);
    }
}
