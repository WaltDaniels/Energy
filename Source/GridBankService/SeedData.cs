using System.Data.Entity.Migrations;
using GridBankData;

namespace GridBankService
{
    public class SeedData
    {
        public static void Create()
        {
            using (var context = new GridBankDb())
            {
                context.GridBankSites.AddOrUpdate(x => x.Id,
                    new GridBankSite {Id = 1, Description = "Hwy49-Harrisburg"},
                    new GridBankSite {Id = 2, Description = "Hwy29-Concord"}
                    );

                context.SaveChanges();
            }
        }
    }
}