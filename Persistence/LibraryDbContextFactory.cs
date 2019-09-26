using Microsoft.EntityFrameworkCore;
using Persistence.Infrastructure;

namespace Persistence
{
    public class LibraryDbContextFactory : DesignTimeDbContextFactoryBase<LibraryDbContext>
    {
        protected override LibraryDbContext CreateNewInstance(DbContextOptions<LibraryDbContext> options)
        {
            return new LibraryDbContext(options);
        }
    }
}