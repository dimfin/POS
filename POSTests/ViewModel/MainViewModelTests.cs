using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using POS.DataProvider;
using POS.Model;
using POS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.ViewModel.Tests
{

    [TestClass()]
    public class MainViewModelTests
    {
        private Mock<IDataProvider> provider;

        [TestInitialize]
        public void Initialize()
        {
            provider = new Mock<IDataProvider>();
            provider.Setup(x => x.GetProducts()).Returns(new List<Product>
            {
                new Product
                {
                    ProductId = 1,
                    Name = "Молоко",
                    Price = 10m
                }
            });
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MainViewModel_ConstructorWithNullTest()
        {
            var m = new MainViewModel(null);
        }

        [TestMethod()]
        public void MainViewModel_AddNewRecordButtonTest()
        {
            var m = new MainViewModel(provider.Object);

            m.SelectedProduct = m.Products[0];
            m.Quantity = 1;

            m.AddRecord.Execute(0);

            // record has been added to DataGrid
            Assert.AreEqual(1, m.Records.Count);
            Assert.AreEqual(m.Products[0].ProductId, m.Records[0].Product.ProductId);

            // add panel has been reinitialized
            Assert.IsNull(m.SelectedProduct);
            Assert.AreEqual(0, m.Quantity);

            // total sum has been updated
            Assert.AreEqual(10, m.TotalSum);
        }

        [TestMethod()]
        public void MainViewModel_PayButtonTest()
        {
            Product p = provider.Object.GetProducts().First();

            var sale = new Sale
            {
                SaleRecords = new List<SaleRecord>
                {
                    new SaleRecord
                    {
                        Product = p,
                        Quantity = 1
                    }
                }
            };

            provider.Setup(x => x.AddSale(It.IsAny<Sale>())).Returns(1);

            var m = new MainViewModel(provider.Object, x => { });
            m.Pay.Execute(0);

            // AddSale method has been called
            provider.Verify(x => x.AddSale(It.IsAny<Sale>()), Times.Once());

            // add panel has been reinitialized
            Assert.IsNull(m.SelectedProduct);
            Assert.AreEqual(0, m.Quantity);

            // DataGrid data has been removed
            Assert.IsFalse(m.Records.Any());

            // total sum has been updated
            Assert.AreEqual(0, m.TotalSum);
        }
    }
}