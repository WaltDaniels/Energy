using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GridBankData
{
    [Table("GridBankSite")]
    public class GridBankSite
    {
        public GridBankSite()
        {
            Usages = new HashSet<Usage>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<Usage> Usages { get; set; }
    }
}