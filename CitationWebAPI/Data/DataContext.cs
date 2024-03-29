﻿using CitationWebAPI.Converter;
using CitationWebAPI.Converters;
using CitationWebAPI.Models;
using Microsoft.EntityFrameworkCore;

/*
 * Translate request objects into SQL queries. Conduit to connect database.
 * 
 */

namespace CitationWebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // Create entity set (table) used by Entity Framework Core
        public DbSet<Citation> Citations => Set<Citation>();
        public DbSet<Driver> Drivers => Set<Driver>();
        public DbSet<Violation> Violations => Set<Violation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
        
        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");

            builder.Properties<TimeOnly>()
                .HaveConversion<TimeOnlyConverter>()
                .HaveColumnType("time");

            base.ConfigureConventions(builder);
        }
    }
}
