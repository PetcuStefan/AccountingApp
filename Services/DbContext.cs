using Microsoft.EntityFrameworkCore;
using AccountingApp.Models;
using System.Collections.Generic;

namespace AccountingApp.Services
{
    public class AppDbContext : DbContext
    {
        public DbSet<Report> Reports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost;Database=MyAppDb;Trusted_Connection=True;");
        }
    }
}
