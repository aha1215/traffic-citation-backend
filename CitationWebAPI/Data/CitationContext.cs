using CitationWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

/**
 * Translate request objects into SQL queries. Conduit to connect database.
 * 
 */

namespace CitationWebAPI.Data
{
    public class CitationContext : DbContext
    {
        public CitationContext(DbContextOptions<CitationContext> options) : base(options) { }

        // To see citation entity as a table in the database add here
        public DbSet<Citation> Citations => Set<Citation>();
    }
}
