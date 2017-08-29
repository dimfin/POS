using POS.ViewModel;
using System.Windows;

namespace POS.View
{
    /// <summary>
    /// Interaction logic for PayView.xaml
    /// </summary>
    public partial class PayView : Window
    {
        public PayView(PayViewModel data)
        {
            InitializeComponent();

            DataContext = data;
        }
    }
}
