using MySql.Data.Entity;
using POS.Model;
using System.Data.Entity;

namespace POS.DataProvider
{
    /// <summary>
    /// EF context
    /// </summary>
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class SaleDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
    }
}
