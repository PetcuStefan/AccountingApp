using AccountingApp.Models;
using AccountingApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AccountingApp.Utilities;
using System.Windows;

namespace AccountingApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Report> Costs { get; set; }
        public ObservableCollection<Report> Revenues { get; set; }

        public ICommand AddCostCommand { get; }
        public ICommand AddRevenueCommand { get; }

        public MainViewModel()
        {
            LoadReports();

            AddCostCommand = new RelayCommand(_ => AddReport(false));
            AddRevenueCommand = new RelayCommand(_ => AddReport(true));
        }

        private void LoadReports()
        {
            using var db = new AppDbContext();
            var reports = db.Reports.ToList();

            Costs = new ObservableCollection<Report>(reports.Where(r => r.value < 0));
            Revenues = new ObservableCollection<Report>(reports.Where(r => r.value > 0));
        }

        public void RefreshReports()
        {
            LoadReports();
            OnPropertyChanged(nameof(Costs));
            OnPropertyChanged(nameof(Revenues));
        }

        private void AddReport(bool isRevenue)
        {
            var window = new Views.AddReportWindow(isRevenue)
            {
                Owner = Application.Current.MainWindow
            };

            if (window.ShowDialog() == true && window.NewReport != null)
            {
                using var db = new AppDbContext();
                db.Reports.Add(window.NewReport);
                db.SaveChanges();
            }

            // Refresh after insert
            LoadReports();
            OnPropertyChanged(nameof(Costs));
            OnPropertyChanged(nameof(Revenues));
        }
    }
}


