using System.Windows;
using MahApps.Metro.Controls;

namespace GridBank
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly double _clickIncrement = .025;
        private readonly int _siteId = 1;
        private readonly MainWindowViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainWindowViewModel();
            DataContext = viewModel;

            //Read current Power Level from GridBankApi
            var apiAdapter = new GridBankApiAdapter();
            viewModel.CurrentValue = apiAdapter.GetCurrentPower(_siteId);
        }

        private void Charge_Click(object sender, RoutedEventArgs e)
        {
            //Send the event to GridBankApi
            var apiAdapter = new GridBankApiAdapter();
            viewModel.CurrentValue = apiAdapter.Charge(_siteId, (decimal) _clickIncrement);

            if (viewModel.CurrentValue > 1) viewModel.CurrentValue = 1;
            if (viewModel.CurrentValue < 0) viewModel.CurrentValue = 0;
        }

        private void Discharge_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Send the event to GridBankApi
            var apiAdapter = new GridBankApiAdapter();
            viewModel.CurrentValue = apiAdapter.Drain(_siteId, (decimal) _clickIncrement);

            if (viewModel.CurrentValue > 1) viewModel.CurrentValue = 1;
            if (viewModel.CurrentValue < 0) viewModel.CurrentValue = 0;
        }
    }
}