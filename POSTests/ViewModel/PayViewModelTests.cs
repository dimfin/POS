using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace POS.ViewModel.Tests
{
    [TestClass()]
    public class PayViewModelTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PayViewModel_ConstructorWithZeroTest()
        {
            var m = new PayViewModel(0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PayViewModel_ConstructorWithNegativeTest()
        {
            var m = new PayViewModel(-2);
        }

        [TestMethod()]
        public void PayViewModel_UpdateDifferenceTest()
        {
            var m = new PayViewModel(7);
            m.GivenSum = 10;
            Assert.AreEqual(3, m.Change);
        }
    }
}