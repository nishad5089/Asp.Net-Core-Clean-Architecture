using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Persistence {
    public class LibraryDbContext : DbContext, ILibraryDbContext {
        public LibraryDbContext (DbContextOptions<LibraryDbContext> options) : base (options) {

        }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Reviewer> Reviewers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        // public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        // {
        //     ChangeTracker.DetectChanges();

        //     foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        //     {
        //         if (entry.State == EntityState.Added)
        //         {
        //             entry.Entity.CreatedBy = _currentUserService.GetUserId();
        //             entry.Entity.Created = _dateTime.Now;

        //         }
        //         else if (entry.State == EntityState.Modified)
        //         {
        //             entry.Entity.LastModifiedBy = _currentUserService.GetUserId();
        //             entry.Entity.LastModified = _dateTime.Now;
        //         }
        //     }

        //     return base.SaveChangesAsync(cancellationToken);
        // }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly (typeof (LibraryDbContext).Assembly);
        }
    }
}