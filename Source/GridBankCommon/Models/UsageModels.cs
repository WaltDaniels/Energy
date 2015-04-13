using System;
using System.Collections.Generic;

namespace GridBankCommon.Models
{
    public class DrainModel
    {
        public int SiteId { get; set; }
        public decimal Amount { get; set; }
    }

    public class ChargeModel
    {
        public int SiteId { get; set; }
        public decimal Amount { get; set; }
    }

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