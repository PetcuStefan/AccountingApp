using AccountingApp.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AccountingApp.Views
{
    public partial class AddReportWindow : Window
    {
        public Report? NewReport { get; private set; }
        private readonly bool _isRevenue;

        public AddReportWindow(bool isRevenue)
        {
            InitializeComponent();
            _isRevenue = isRevenue;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ValueBox.Text, out double value))
            {
                // If it's a cost, ensure the value is negative
                if (!_isRevenue && value > 0)
                    value = -value;

                NewReport = new Report
                {
                    value = value,
                    path = string.IsNullOrWhiteSpace(PathBox.Text) ? null : PathBox.Text,
                    date = DateOnly.FromDateTime(DatePicker.SelectedDate ?? DateTime.Now)
                };

                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid number for Value.");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

