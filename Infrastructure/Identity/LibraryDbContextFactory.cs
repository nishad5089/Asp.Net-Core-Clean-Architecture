using Infrastructure;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Identity
{
    public class LibraryDbContextFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>
    {
        protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
        {
            return new ApplicationDbContext(options);
        }
    }
}