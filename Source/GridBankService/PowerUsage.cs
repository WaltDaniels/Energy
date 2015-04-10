using GridBankData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBankService
{
    public class PowerUsage
    {
        public decimal Drain(int SiteId, decimal Amount)
        {
            using (var context = new GridBankDb())
            {
                var allSites = context.GridBankSites;


                var site = context.GridBankSites.Find(SiteId);
                if (site == null) throw new Exception("Site not found!");

                var lastReading =
                    site.Usages
                        .OrderByDescending(x => x.TimeStamp)
                        .FirstOrDefault();

                decimal lastPower = (lastReading != null ? lastReading.CurrentPower : 0);


                var usage = new Usage()
                {
                    GridBankSiteId = SiteId,
                    TimeStamp = DateTime.Now,
                    CurrentPower = lastPower - Amount,
                    IsNew = true
                };

                site.Usages.Add(usage);
                context.SaveChanges();

                return usage.CurrentPower;
            }
        }

        public decimal Charge(int SiteId, decimal Amount)
        {
            using (var context = new GridBankDb())
            {
                var lastReading =
                    context.Usages.Where(x => x.GridBankSiteId == SiteId)
                        .OrderByDescending(x => x.TimeStamp)
                        .FirstOrDefault();

                decimal lastPower = (lastReading != null ? lastReading.CurrentPower : 0);


                var usage = new Usage()
                {
                    GridBankSiteId = SiteId,
                    TimeStamp = DateTime.Now,
                    CurrentPower = lastPower + Amount,
                    IsNew = true
                };

                context.Usages.Add(usage);
                context.SaveChanges();

                return usage.CurrentPower;
            }
        }

    }
}
