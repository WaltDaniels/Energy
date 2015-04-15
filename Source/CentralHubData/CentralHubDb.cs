using System.Data.Entity;

namespace CentralHubData
{
    public partial class CentralHubDb : DbContext
    {
        public CentralHubDb()
            : base("name=CentralHubDb")
        {
        }

        public virtual DbSet<GridBankSite> GridBankSites { get; set; }
        public virtual DbSet<Usage> Usages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GridBankSite>()
                .HasMany(e => e.Usages)
                .WithRequired(e => e.GridBankSite)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usage>()
                .Property(x => x.CurrentPower)
                .HasPrecision(18, 3);
        }
    }
}
