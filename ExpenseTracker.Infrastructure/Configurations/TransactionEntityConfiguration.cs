using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Infrastructure.Configurations;

public class TransactionEntityConfiguration : IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Amount)
            .IsRequired();

        builder.Property(t => t.BalanceBeforeTransaction)
            .IsRequired();

        builder.Property(t => t.Date)
            .IsRequired();

        builder.HasOne<UserPageEntity>()
            .WithMany()
            .HasForeignKey(t => t.UserPageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<CategoryEntity>()
            .WithMany()
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
