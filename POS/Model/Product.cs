using System.Collections.Generic;

namespace POS.Model
{
    /// <summary>
    /// Product model class for EF Code First
    /// </summary>
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal WarehouseQuantity { get; set; }

        public virtual List<SaleRecord> SaleRecords { get; set; }
    }
}
