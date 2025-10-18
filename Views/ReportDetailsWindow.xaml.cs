using AccountingApp.Models;
using AccountingApp.Services;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace AccountingApp.Views
{
    public partial class ReportDetailsWindow : Window
    {
        private readonly Report _report;

        public ReportDetailsWindow(Report report)
        {
            InitializeComponent();
            _report = report;
            DataContext = report;
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_report.path))
            {
                MessageBox.Show("No file associated with this report.");
                return;
            }

            try
            {
                string sourceFile = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "AccountingApp",
                    _report.path
                );

                if (!File.Exists(sourceFile))
                {
                    MessageBox.Show("File not found in storage.");
                    return;
                }

                var dialog = new SaveFileDialog
                {
                    FileName = Path.GetFileName(sourceFile),
                    Filter = "All Files|*.*"
                };

                if (dialog.ShowDialog() == true)
                {
                    File.Copy(sourceFile, dialog.FileName, overwrite: true);
                    MessageBox.Show("File downloaded successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading file: {ex.Message}");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this entry?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                using var db = new AppDbContext();
                var toDelete = db.Reports.Find(_report.id);

                if (toDelete != null)
                {
                    db.Reports.Remove(toDelete);
                    db.SaveChanges();
                }

                MessageBox.Show("Entry deleted successfully.");
                DialogResult = true;
                Close();
            }
        }
    }
}
