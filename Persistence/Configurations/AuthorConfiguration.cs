using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.AuthorId);

            builder.Property(a => a.AuthorId).ValueGeneratedOnAdd();
            builder.Property(a => a.FirstName)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(a => a.LastName)
            .IsRequired()
            .HasMaxLength(200);
        }
    }
}