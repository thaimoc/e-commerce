using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;

namespace eCommerce.DAL.ExternalConfigurations
{
    public class MigrationsHistoryTableContext : HistoryContext
    {
        public MigrationsHistoryTableContext(DbConnection existingConnection, string defaultSchema) : base(existingConnection, defaultSchema)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("admin");
            modelBuilder.Entity<HistoryRow>().ToTable("MigrationsHistory", "admin");
            modelBuilder.Entity<HistoryRow>().Property(p => p.ContextKey).HasMaxLength(150);
        }
    }
}