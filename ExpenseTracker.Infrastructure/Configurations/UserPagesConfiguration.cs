using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Infrastructure.Configurations;

public class UserPagesConfiguration : IEntityTypeConfiguration<UserPageEntity>
{
    public void Configure(EntityTypeBuilder<UserPageEntity> builder)
    {
        builder.ToTable("UserPages");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Balance)
            .IsRequired();

        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(p => p.UserId);
    }
}
