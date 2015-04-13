using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralHubService.Models
{
    public class UsageModel
    {
        public class Details
        {
            public int SiteId { get; set; }
            public List<UsageEntry> UsageEntries { get; set; }
        }

        public class UsageEntry
        {
            public Guid IdGuid { get; set; }
            public DateTime TimeStamp { get; set; }
            public decimal CurrentPower { get; set; }
        }
    }
}
