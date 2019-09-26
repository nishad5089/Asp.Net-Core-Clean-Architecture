using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(a => a.CountryId);

            builder.Property(a => a.CountryId).ValueGeneratedOnAdd();
            builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(50);

        }
    }
}