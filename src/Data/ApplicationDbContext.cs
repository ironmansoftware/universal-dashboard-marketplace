using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Data.Seeding;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly IEnumerable<IDataSeeder> _seeders;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IEnumerable<IDataSeeder> seeders)
            : base(options)
        {
            _seeders = seeders;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagItem> TagItems { get; set; }
        public DbSet<UniversalDashboardVersion> UniversalDashboardVersions { get; set; }
        public DbSet<DatabaseVersion> DatabaseVersions { get; set; }
        public DbSet<ModuleInfo> ModuleInfo { get; set; }

        public void EnsureSeedData()
        {
            if (!DatabaseVersions.Any())
            {
                DatabaseVersions.Add(new DatabaseVersion { Version = 0 });
                SaveChanges();
            }

            var currentDatabaseVersion = DatabaseVersions.Max(m => m.Version);

            foreach(var seeder in _seeders)
            {
                if (seeder.Version > currentDatabaseVersion)
                {
                    seeder.Seed(this);
                    DatabaseVersions.Add(new DatabaseVersion { Version = seeder.Version });
                    SaveChanges();
                    currentDatabaseVersion = seeder.Version;
                }
            }
        }
    }
}

