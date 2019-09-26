using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(e => new { e.BookId, e.CategoryId })
            .ForSqlServerIsClustered(false);

            builder.HasOne(d => d.Book)
             .WithMany(p => p.BookCategories)
             .HasForeignKey(d => d.BookId)
             .OnDelete(DeleteBehavior.ClientSetNull)
             .HasConstraintName("FK_BookCategories_Books");

            builder.HasOne(d => d.Category)
                .WithMany(p => p.BookCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookCategories_Categories");
        }
    }
}