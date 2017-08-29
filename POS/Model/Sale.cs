using System;
using System.Collections.Generic;

namespace POS.Model
{
    /// <summary>
    /// Sale model class for EF Code First
    /// </summary>
    public class Sale
    {
        public int SaleId { get; set; }
        public DateTime Date { get; set; }

        public virtual List<SaleRecord> SaleRecords { get; set; }
    }
}
