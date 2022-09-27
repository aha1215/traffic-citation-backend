using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CitationWebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // To see entity as a table in database add here
        public DbSet<Citation> Citations => Set<Citation>();
    }
}
