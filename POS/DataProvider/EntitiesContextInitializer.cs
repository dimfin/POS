using POS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DataProvider
{
    /// <summary>
    /// Database initializer
    /// </summary>
    public class EntitiesContextInitializer : CreateDatabaseIfNotExists<SaleDbContext>
    {
        /// <summary>
        /// Set up database
        /// Add some products
        /// </summary>
        /// <param name="context">context to use</param>
        protected override void Seed(SaleDbContext context)
        {
            context.Products.AddRange(new Product[] {
                 new Product
                 {
                     Name = "Молоко \"Фермерское подворье\" 3.2%",
                     Price = 64m,
                     WarehouseQuantity = 10
                 },
                 new Model.Product
                 {
                     Name = "Кетчуп Хайнц острый",
                     Price = 95.90m,
                     WarehouseQuantity = 10
                 },
                 new Model.Product
                 {
                     Name = "Майонез Махеев Провансаль",
                     Price = 34.50m,
                     WarehouseQuantity = 10
                 },
            });
        }
    }
}
