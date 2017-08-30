using System.ComponentModel;

namespace POS.ViewModel
{
    /// <summary>
    /// Base class for ViewModel
    /// Implements INotifyPropertyChanged interface
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
