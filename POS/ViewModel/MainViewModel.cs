using POS.DataProvider;
using POS.Model;
using POS.View;
using POS.ViewModel.Command;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace POS.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        // Open dialog stub. Initialized by constructor. Is used by tests
        private readonly Action<decimal> openPayDialog;
        // data provider. Initialized by constructor
        private readonly IDataProvider dataProvider;

        // Selected product of Add Sale Record
        private Product selectedProduct;
        // Quantity of Add Sale Record 
        private decimal quantity;

        #endregion

        #region Properties

        // Collection of sale records for DataGrid
        public ObservableCollection<SaleRecord> Records { get; } = new ObservableCollection<Model.SaleRecord>();
        // List of Products for ComboBox
        public List<Product> Products { get; private set; }

        // calculated property for TotalSum Lable
        public decimal TotalSum => Records.Sum(x => x.Quantity * x.Product.Price);

        public Product SelectedProduct
        {
            get
            {
                return selectedProduct;
            }

            set
            {
                selectedProduct = value;
                RaisePropertyChangedEvent("SelectedProduct");
            }
        }

        public decimal Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
                RaisePropertyChangedEvent("Quantity");
            }
        }

        // Add record button of Add Sale Record 
        public RelayCommand AddRecord => new RelayCommand(AddSaleRecord);

        // PAy in cash button
        public RelayCommand Pay => new RelayCommand(SaveSale);
        #endregion

        #region Constructors

        // constructor for testing
        public MainViewModel(IDataProvider dataProvider, Action<decimal> openPayDialog = null)
        {
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");

            this.openPayDialog = openPayDialog ?? (s => new PayView(new PayViewModel(s)).ShowDialog());

            this.dataProvider = dataProvider;

            // register for collection change event
            Records.CollectionChanged += Records_CollectionChanged;

            // fill list of products
            Products = dataProvider.GetProducts();
        }

        // constructor for work
        public MainViewModel() : this(new EFDataProvider(), null) { }
        #endregion

        #region Private_Methods

        /// <summary>
        /// If Records is changed, send notification to DataGrid
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">event data</param>
        private void Records_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChangedEvent("TotalSum");
        }

        /// <summary>
        /// Add Sale Record button is pressed
        /// </summary>
        private void AddSaleRecord()
        {
            // add to sale
            Records.Add(new SaleRecord
            {
                Product = SelectedProduct,
                Quantity = Quantity
            });

            // Clear Add Sale Record controls
            SelectedProduct = null;
            Quantity = 0;
        }

        /// <summary>
        /// PAy in Cash button is pressed
        /// </summary>
        private void SaveSale()
        {
            // save sale
            dataProvider.AddSale(new Sale
            {
                Date = DateTime.Now,
                SaleRecords = Records.ToList()
            });

            // open PayView dialog
            openPayDialog(TotalSum);

            // prepare to next sale
            Records.Clear();
            SelectedProduct = null;
            Quantity = 0;
        }
    } 
    #endregion
}
