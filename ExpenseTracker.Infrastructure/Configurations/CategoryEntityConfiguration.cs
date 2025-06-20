using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExpenseTracker.Infrastructure.Configurations;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.Id);

        var typeConvertor = new EnumToStringConverter<CategoryType>();
        builder
            .Property(c => c.Type)
            .HasConversion(typeConvertor);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(35);

        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(c => c.UserId);
    }
}
