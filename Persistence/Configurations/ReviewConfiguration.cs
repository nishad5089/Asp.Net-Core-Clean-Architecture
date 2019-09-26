using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(a => a.ReviewId);

            builder.Property(a => a.ReviewId).ValueGeneratedOnAdd();
            builder.Property(a => a.Headline)
            .IsRequired()
            .HasMaxLength(200);

            builder.Property(a => a.ReviewText)
            .IsRequired()
            .HasMaxLength(2000);

            builder.Property(a => a.Rating)
            .IsRequired();

            builder.HasOne(a => a.Book)
            .WithMany(b => b.Reviews)
            .HasForeignKey(x => x.BookId)
            .HasConstraintName("FK_Book_Reviews");

            builder.HasOne(a => a.Reviewer)
            .WithMany(b => b.Reviews)
            .HasForeignKey(x => x.ReviewerId)
            .HasConstraintName("FK_Reviewer_Reviews");
        }
    }
}