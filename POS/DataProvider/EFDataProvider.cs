using POS.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POS.DataProvider
{
    /// <summary>
    /// Default implementation of the interface
    /// Uses Entity Framework
    /// </summary>
    public class EFDataProvider : IDataProvider
    {
        private SaleDbContext context;

        // constructor for unit tests
        public EFDataProvider(SaleDbContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            this.context = context;
        }

        // constructor for work
        public EFDataProvider() : this(new SaleDbContext()) { }

        /// <summary>
        /// Save a sale to database
        /// </summary>
        /// <param name="sale">sale to save</param>
        /// <returns>ID of created record</returns>
        public int AddSale(Sale sale)
        {
            // save sale
            context.Sales.Add(sale);
            // update inventory
            sale.SaleRecords.ToList().ForEach(r => r.Product.WarehouseQuantity -= r.Quantity);
            context.SaveChanges();

            return sale.SaleId;
        }

        /// <summary>
        /// Get all products from database
        /// </summary>
        /// <returns>list of products</returns>
        public List<Product> GetProducts()
        {
            return context.Products.ToList();
        }

        /// <summary>
        /// Dispose context
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
