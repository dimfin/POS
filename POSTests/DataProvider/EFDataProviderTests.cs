using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using POS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace POS.DataProvider.Tests
{
    [TestClass()]
    public class EFDataProviderTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EFDataProvider_ConstructorWithNullParameterTest()
        {
            var provider = new EFDataProvider(null);
        }

        [TestMethod()]
        public void EFDataProvider_AddSaleTest()
        {
            var mockProductSet = new Mock<DbSet<Product>>();
            var mockSaleSet = new Mock<DbSet<Sale>>();
            var mockContext = new Mock<SaleDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockProductSet.Object);
            mockContext.Setup(m => m.Sales).Returns(mockSaleSet.Object);

            var service = new EFDataProvider(mockContext.Object);
            var sale = new Sale
            {
                SaleRecords = new List<SaleRecord>
                {
                    new SaleRecord
                    {
                        Product = new Product
                        {
                            ProductId = 1,
                            WarehouseQuantity = 10
                        },
                        Quantity = 8
                    }
                }
            };
            service.AddSale(sale);

            // sale has been added
            mockSaleSet.Verify(m => m.Add(It.IsAny<Sale>()), Times.Once());
            // sale has been saved
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
            // product quantity has been updated correctly
            Assert.AreEqual(2, sale.SaleRecords[0].Product.WarehouseQuantity);
        }

        [TestMethod()]
        public void EFDataProvider_GetProductsTest()
        {
            var data = new List<Product>
            {
                new Product {
                    ProductId = 1,
                    Name = "Молоко",
                    Price =45m,
                    WarehouseQuantity = 10 }
            }.AsQueryable();

            var productDbSet = new Mock<DbSet<Product>>();
            productDbSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<SaleDbContext>();
            mockContext.Setup(c => c.Products).Returns(productDbSet.Object);

            var service = new EFDataProvider(mockContext.Object);
            var products = service.GetProducts();

            // returned list of products is data
            CollectionAssert.AreEqual(products, data.ToList());
        }
    }
}