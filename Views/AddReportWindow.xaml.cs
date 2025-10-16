using AccountingApp.Models;
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

        // DragEnter – highlights the drop zone
        private void FileDropZone_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                FileDropZone.BorderBrush = System.Windows.Media.Brushes.Green;
            }
        }

        // DragLeave – resets the drop zone border
        private void FileDropZone_DragLeave(object sender, DragEventArgs e)
        {
            FileDropZone.BorderBrush = new System.Windows.Media.SolidColorBrush(
                (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#4CAF50"));
        }

        // Drop – handle file drop
        private void FileDropZone_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
            {
                string file = files[0];
                string ext = Path.GetExtension(file).ToLower();

                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".pdf")
                {
                    try
                    {
                        // ✅ Save uploads to MyDocuments\AccountingApp\uploads
                        string uploadsDir = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                            "AccountingApp",
                            "uploads"
                        );

                        if (!Directory.Exists(uploadsDir))
                            Directory.CreateDirectory(uploadsDir);

                        // Create a unique file name
                        string newFileName = $"{Guid.NewGuid()}{ext}";
                        string destPath = Path.Combine(uploadsDir, newFileName);

                        // Copy the file
                        File.Copy(file, destPath, overwrite: false);

                        // Store relative path for the database
                        _savedFilePath = Path.Combine("uploads", newFileName);

                        // Update UI
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
                    MessageBox.Show("Please drop a JPG, PNG, or PDF file.", "Invalid File Type", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        // Save button – create the Report object
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

        // Cancel button
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
