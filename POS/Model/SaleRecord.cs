namespace POS.Model
{
    /// <summary>
    /// Sale Record model class for EF Code First
    /// </summary>
    public class SaleRecord
    {
        public int SaleRecordId { get; set; }
        public decimal Quantity { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
