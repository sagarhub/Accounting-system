using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Ams.Models;
using Ams.ViewModels;

namespace Ams.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
         public DbSet<Ledger> ledgers { get; set; }
        public DbSet<ParentGroup> parentGroups { get; set; }
        public DbSet<Bank> banks { get; set; }

        public DbSet<Income> incomes { get; set; } 
        public DbSet<Expenses> expenses { get; set; }
        public DbSet<Payable> payables { get; set; }
        public DbSet<Receivable> receivables { get; set; }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<DateTime>()
                .HaveColumnType("timestamp without time zone");
        }
        public DbSet<Transactions> transactions { get; set; }
        public DbSet<TxnType> txnTypes { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<Receipt> receipts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParentGroup>().HasData(
                new ParentGroup
                {
                    id = 1,
                    name = "Assets",
                    code = 1
                },
                new ParentGroup
                {
                    id = 2,
                    name = "Liabilities",
                    code = 2
                },
                new ParentGroup
                {
                    id = 3,
                    name = "Income",
                    code = 3
                },
                new ParentGroup
                {
                    id = 4,
                    name = "Expenses",
                    code = 4
                });
        }
    }
}
