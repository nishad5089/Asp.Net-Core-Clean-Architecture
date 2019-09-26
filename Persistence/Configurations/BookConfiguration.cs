using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(a => a.BookId);

            builder.Property(a => a.BookId).ValueGeneratedOnAdd();
            builder.Property(a => a.Isbn)
            .IsRequired()
            .HasMaxLength(10);

            builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(200);

        }
    }
}