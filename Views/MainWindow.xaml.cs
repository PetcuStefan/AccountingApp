using AccountingApp.Models;
using AccountingApp.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountingApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        // Handles double-click on a Cost row
        private void CostGrid_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((DataGrid)sender).SelectedItem is Report selectedReport)
            {
                var detailsWindow = new ReportDetailsWindow(selectedReport)
                {
                    Owner = this
                };

                if (detailsWindow.ShowDialog() == true)
                {
                    // Refresh data in MainViewModel after an update or delete
                    ((MainViewModel)DataContext).RefreshReports();
                }
            }
        }

        // Handles double-click on a Revenue row
        private void RevenueGrid_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((DataGrid)sender).SelectedItem is Report selectedReport)
            {
                var detailsWindow = new ReportDetailsWindow(selectedReport)
                {
                    Owner = this
                };

                if (detailsWindow.ShowDialog() == true)
                {
                    ((MainViewModel)DataContext).RefreshReports();
                }
            }
        }
    }
}
