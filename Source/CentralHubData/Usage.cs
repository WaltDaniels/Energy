using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralHubData
{
    [Table("Usage")]
    public class Usage
    {
        [Key]
        public int Id { get; set; }

        public Guid IdGuid { get; set; }
        public int GridBankSiteId { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal CurrentPower { get; set; }
        public bool IsNew { get; set; }
        public virtual GridBankSite GridBankSite { get; set; }
    }
}