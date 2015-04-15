using System;
using System.Collections.Generic;
using System.Linq;
using GridBankCommon.Models;
using GridBankData;

namespace GridBankService
{
    public class PowerUsage
    {
        public decimal Drain(int SiteId, decimal Amount)
        {
            using (var context = new GridBankDb())
            {
                var site = context.GridBankSites.Find(SiteId);
                if (site == null) throw new Exception("Site not found!");

                var lastReading =
                    site.Usages
                        .OrderByDescending(x => x.TimeStamp)
                        .FirstOrDefault();

                decimal lastPower = (lastReading != null ? lastReading.CurrentPower : 0);


                var usage = new Usage()
                {
                    IdGuid = Guid.NewGuid(),
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
                var site = context.GridBankSites.Find(SiteId);
                if (site == null) throw new Exception("Site not found!");

                var lastReading =
                    site.Usages
                        .OrderByDescending(x => x.TimeStamp)
                        .FirstOrDefault();

                decimal lastPower = (lastReading != null ? lastReading.CurrentPower : 0);


                var usage = new Usage()
                {
                    IdGuid = Guid.NewGuid(),
                    GridBankSiteId = SiteId,
                    TimeStamp = DateTime.Now,
                    CurrentPower = lastPower + Amount,
                    IsNew = true
                };

                site.Usages.Add(usage);
                context.SaveChanges();

                return usage.CurrentPower;
            }
        }

        public List<UsageEntry> GetEntries(int SiteId, DateTime detailsStartingDateTime)
        {
            using (var context = new GridBankDb())
            {
                var usageEntries =
                    context.Usages
                           .Where(x => x.GridBankSiteId == SiteId && x.TimeStamp > detailsStartingDateTime)
                           .OrderBy(x => x.TimeStamp)
                           .Select(x => new UsageEntry
                           {
                               IdGuid = x.IdGuid,
                               TimeStamp = x.TimeStamp,
                               CurrentPower = x.CurrentPower
                           })
                           .ToList();

                return usageEntries;
            }
        }

        public decimal GetCurrentPower(int siteId)
        {
            using (var context = new GridBankDb())
            {
                var site = context.GridBankSites.Find(siteId);
                if (site == null) throw new Exception("Site not found!");

                var lastUsage = site.Usages.OrderByDescending(x => x.TimeStamp).FirstOrDefault();
                decimal lastPower = lastUsage != null ? lastUsage.CurrentPower : 0;

                return lastPower;
            }
        }
    }
}
