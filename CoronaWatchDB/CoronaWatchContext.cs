namespace CoronaWatchDB
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Common;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    public partial class CoronaWatchContext : DbContext
    {
        public CoronaWatchContext()
        {
        }

        public virtual DbSet<RegionDB> RegionDBs { get; set; }
        public virtual DbSet<ReportDB> ReportDBs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=CoronaWatchDB.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegionDB>()
                    .Property(e => e.Name)
                    .IsUnicode(false);

            modelBuilder.Entity<RegionDB>()
                    .Property(e => e.Slug)
                    .IsUnicode(false);

            modelBuilder.Entity<RegionDB>()
                    .Property(e => e.ISOCode)
                    .IsUnicode(false);

            modelBuilder.Entity<ReportDB>()
                .Property(e => e.ISOCode)
                .IsUnicode(false);
        }
    }
}
