namespace CoronaWatchDB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CoronaWatchContext : DbContext
    {
        public CoronaWatchContext()
            : base("name=CoronaWatchContext")
        {
        }

        public virtual DbSet<RegionDB> RegionDBs { get; set; }
        public virtual DbSet<ReportDB> ReportDBs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
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
