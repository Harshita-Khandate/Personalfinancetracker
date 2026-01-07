using PersonalFinanceTracker.Models;
using Microsoft.EntityFrameworkCore;
namespace PersonalFinanceTracker.DataBase

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Users> Users {  get; set; }
        public DbSet<Income> Income {  get; set; }
        public DbSet<IncomeCategory> IncomeCategory { get; set; }
    }
}
