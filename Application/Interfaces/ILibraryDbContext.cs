using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface ILibraryDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<Reviewer> Reviewers { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<BookAuthor> BookAuthors { get; set; }
        DbSet<BookCategory> BookCategories { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}