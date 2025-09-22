using System.Windows;

namespace AccountingApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new AccountingApp.ViewModels.MainViewModel();
        }
    }
}
