using System.Data.Entity.Migrations;
using CentralHubData;

namespace CentralHubService
{
    public class SeedData
    {
        public static void Create()
        {
            using (var context = new CentralHubDb())
            {
                context.GridBankSites.AddOrUpdate(x => x.Id,
                    new GridBankSite
                    {
                        Id = 1,
                        Description = "Hwy49-Harrisburg",
                        ApiUrl = "http://localhost:62056/",
                        IsActive = true
                    },
                    new GridBankSite
                    {
                        Id = 2,
                        Description = "Hwy29-Concord",
                        ApiUrl = "http://localhost:62056/",
                        IsActive = true
                    }
                    );

                context.SaveChanges();
            }
        }
    }
}