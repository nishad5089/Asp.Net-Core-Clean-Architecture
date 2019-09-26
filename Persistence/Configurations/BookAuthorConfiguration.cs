using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.HasKey(e => new { e.BookId, e.AuthorId })
            .ForSqlServerIsClustered(false);

            builder.HasOne(d => d.Book)
             .WithMany(p => p.BookAuthors)
             .HasForeignKey(d => d.BookId)
             .OnDelete(DeleteBehavior.ClientSetNull)
             .HasConstraintName("FK_BookAuthors_Books");

            builder.HasOne(d => d.Author)
                .WithMany(p => p.BookAuthors)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookAuthors_Authors");
        }
    }
}