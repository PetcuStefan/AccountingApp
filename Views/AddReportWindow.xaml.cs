using AccountingApp.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace AccountingApp.Views
{
    public partial class AddReportWindow : Window
    {
        public Report? NewReport { get; private set; }
        private readonly bool _isRevenue;
        private string? _savedFilePath;

        public AddReportWindow(bool isRevenue)
        {
            InitializeComponent();
            _isRevenue = isRevenue;
        }

        // 🟩 Shared method to process a selected/dropped file
        private void ProcessFile(string file)
        {
            string ext = Path.GetExtension(file).ToLower();

            if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".pdf")
            {
                try
                {
                    // Save uploads to Documents\AccountingApp\uploads
                    string uploadsDir = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        "AccountingApp",
                        "uploads"
                    );

                    if (!Directory.Exists(uploadsDir))
                        Directory.CreateDirectory(uploadsDir);

                    string newFileName = $"{Guid.NewGuid()}{ext}";
                    string destPath = Path.Combine(uploadsDir, newFileName);

                    File.Copy(file, destPath, overwrite: false);

                    _savedFilePath = Path.Combine("uploads", newFileName);

                    FileNameText.Text = $"Uploaded: {Path.GetFileName(file)}";
                    FileDropZone.BorderBrush = System.Windows.Media.Brushes.Green;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving file: {ex.Message}", "File Upload Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please choose a JPG, PNG, or PDF file.", "Invalid File Type", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // DragEnter
        private void FileDropZone_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                FileDropZone.BorderBrush = System.Windows.Media.Brushes.Green;
            }
        }

        // DragLeave
        private void FileDropZone_DragLeave(object sender, DragEventArgs e)
        {
            FileDropZone.BorderBrush = new System.Windows.Media.SolidColorBrush(
                (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#4CAF50"));
        }

        // Drop
        private void FileDropZone_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
            {
                ProcessFile(files[0]);
            }
        }

        // 🟦 Click event – open File Picker
        private void FileDropZone_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select File",
                Filter = "Supported Files|*.jpg;*.jpeg;*.png;*.pdf",
                Multiselect = false
            };

            if (dialog.ShowDialog() == true)
            {
                ProcessFile(dialog.FileName);
            }
        }

        // Save
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ValueBox.Text, out double value))
            {
                if (!_isRevenue && value > 0)
                    value = -value;

                NewReport = new Report
                {
                    value = value,
                    path = _savedFilePath,
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

        // Cancel
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
