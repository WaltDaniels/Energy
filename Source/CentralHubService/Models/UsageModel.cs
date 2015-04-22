using System;
using System.Collections.Generic;

namespace CentralHubService.Models
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