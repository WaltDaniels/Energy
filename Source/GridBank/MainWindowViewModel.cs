using System.ComponentModel;

namespace GridBank
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private double _currentValue;

        public double CurrentValue
        {

            get { return _currentValue; }
            set
            {
                if (value == _currentValue)
                    return;

                _currentValue = value;
                OnPropertyChanged("CurrentValue");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
