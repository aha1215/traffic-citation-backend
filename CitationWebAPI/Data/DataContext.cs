﻿using CitationWebAPI.Converter;
using CitationWebAPI.Converters;
using CitationWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

/**
 * Translate request objects into SQL queries. Conduit to connect database.
 * 
 */

namespace CitationWebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // To see entity as a table in the database add here
        public DbSet<Citation> Citations => Set<Citation>();
        public DbSet<Driver> Drivers => Set<Driver>();
        public DbSet<User> Users => Set<User>();

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