using System.ComponentModel;

namespace GridBank
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private double _currentValue;
        private double _currentPercent;

        public double CurrentValue
        {

            get { return _currentValue; }
            set
            {
                if (value == _currentValue)
                    return;

                _currentValue = value;
                CurrentPercent = value*100;
                OnPropertyChanged("CurrentValue");
            }
        }

        public double CurrentPercent
        {
            get { return _currentPercent; }
            set
            {
                if (value == _currentPercent)
                    return;

                _currentPercent = value;
                OnPropertyChanged("CurrentPercent");
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
