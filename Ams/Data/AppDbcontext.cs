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
    }
}
