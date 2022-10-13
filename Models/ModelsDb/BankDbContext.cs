using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelsDb
{
    public class BankDbContext : DbContext
    {
        public DbSet<AccountDb> Accounts { get; set; }
        public DbSet<EmployeeDb> Employees { get; set; }
        public DbSet<ClientDb> Clients { get; set; }
        public DbSet<CurrencyDb> Currency { get; set; }

        public BankDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;" +
                "Port=5433;" +
                "Database=BankSystem;" +
                "Username=postgres;" +
                "Password=super200;");
        }
    }
}
