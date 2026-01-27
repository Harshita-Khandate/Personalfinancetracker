using PersonalFinanceTracker.Models;
using Microsoft.EntityFrameworkCore;
namespace PersonalFinanceTracker.DataBase { 

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Income> Incomes { get; set; }
        public DbSet<IncomeCategory> IncomeCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Users> Users {  get; set; }
        public DbSet<Goal> Goal { get; set; }

    }
}
