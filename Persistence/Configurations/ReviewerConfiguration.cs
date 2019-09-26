using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ReviewerConfiguration : IEntityTypeConfiguration<Reviewer>
    {
        public void Configure(EntityTypeBuilder<Reviewer> builder)
        {
            builder.HasKey(a => a.ReviewerId);

            builder.Property(a => a.ReviewerId).ValueGeneratedOnAdd();
            builder.Property(a => a.FirstName)
                       .IsRequired()
                       .HasMaxLength(100);
            builder.Property(a => a.LastName)
                       .IsRequired()
                       .HasMaxLength(200);
        }
    }
}