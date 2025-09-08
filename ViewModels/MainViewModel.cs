using AccountingApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AccountingApp.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Report> Costs { get; } = new ObservableCollection<Report>();
        public ObservableCollection<Report> Revenues { get; } = new ObservableCollection<Report>();

        public MainViewModel()
        {
            // Sample data (replace with DB fetch)
            var sampleReports = new[]
            {
                new Report { id = 1, value = -120.50, path = "http://example.com/cost1", date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)) },
                new Report { id = 2, value = 300.75, path = "http://example.com/revenue1", date = DateOnly.FromDateTime(DateTime.Now) },
                new Report { id = 3, value = -50.00, path = "http://example.com/cost2", date = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)) },
                new Report { id = 4, value = 1000.00, path = "http://example.com/revenue2", date = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)) },
            };

            foreach (var report in sampleReports)
            {
                if (report.value < 0)
                    Costs.Add(report);
                else if (report.value > 0)
                    Revenues.Add(report);
            }
        }
    }
}


