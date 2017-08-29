using POS.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.DataProvider
{
    /// <summary>
    /// Interface for classes that provide data for the app
    /// </summary>
    public interface IDataProvider : IDisposable
    {
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of all products</returns>
        List<Product> GetProducts();

        /// <summary>
        /// Save a sale
        /// </summary>
        /// <param name="sale">sale to save</param>
        /// <returns>ID of the saved sale</returns>
        int AddSale(Sale sale);
    }
}
