namespace GridBankData
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GridBankDb : DbContext
    {
        public GridBankDb()
            : base("name=GridBankDb")
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
        }
    }
}
