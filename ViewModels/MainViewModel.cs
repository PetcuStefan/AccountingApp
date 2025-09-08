using AccountingApp.Models;
using AccountingApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AccountingApp.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Report> Reports { get; } = new ObservableCollection<Report>();

        public MainViewModel()
        {
            // If you want to load from DB (requires AppDbContext ready):
            // using var db = new AppDbContext();
            // var list = db.Reports.ToList();
            // foreach (var r in list) Reports.Add(r);

            // Temporary sample data:
            Reports.Add(new Report { id = 1, value = 123.45, path = "http://example.com/report", date = DateOnly.FromDateTime(DateTime.Now) });
        }
    }
}

