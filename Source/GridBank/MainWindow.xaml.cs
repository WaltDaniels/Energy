using MahApps.Metro.Controls;
using System.ComponentModel;

namespace GridBank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainWindowViewModel viewModel;

        private double _clickIncrement = .025;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainWindowViewModel();
            DataContext = viewModel;

            //TODO: Read current Power Level from GridBankApi

        }

        private void Charge_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (viewModel.CurrentValue <= (1 - _clickIncrement))
            {
                viewModel.CurrentValue = viewModel.CurrentValue + .025;
            }
            else
            {
                viewModel.CurrentValue = 1;
            }

            //TODO: Send the event to GridBankApi
        }

        private void Discharge_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (viewModel.CurrentValue >= _clickIncrement)
            {
                viewModel.CurrentValue = viewModel.CurrentValue - .025;
            }
            else
            {
                viewModel.CurrentValue = 0;
            }

            //TODO: Send the event to GridBankApi
        }
    }

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private double _currentValue;

        public MainWindowViewModel()
        {
            CurrentValue = .65;
        }

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
