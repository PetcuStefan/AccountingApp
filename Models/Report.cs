using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Models
{
    public class Report
    {
        public int id {  get; set; }
        public double value { get; set; }
        public string? path { get; set; }
        public DateOnly date { get; set; }
    }
}
