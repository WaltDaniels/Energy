namespace GridBankApi.Models
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
}