using System;

namespace POS.ViewModel
{
    /// <summary>
    /// Pay in Cash modal window
    /// </summary>
    public class PayViewModel : ViewModelBase
    {
        #region Fields

        // value from TextBox
        private decimal givenSum;
        #endregion

        #region Properties

        // Sale sum. Initialized by constructor.
        public decimal TotalSum { get; private set; }

        // calculated property. Change
        public decimal Difference => GivenSum - TotalSum;

        // property for givenSum variable
        public decimal GivenSum
        {
            get
            {
                return givenSum;
            }

            set
            {
                givenSum = value;
                RaisePropertyChangedEvent("GivenSum");

                // update Difference value
                RaisePropertyChangedEvent("Difference");
            }
        }
        #endregion

        #region Constructors
        public PayViewModel(decimal sum)
        {
            if (sum <= 0) throw new ArgumentOutOfRangeException("sum");

            TotalSum = sum;
        } 
        #endregion
    }
}
