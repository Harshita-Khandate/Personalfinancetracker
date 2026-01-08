using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Empty Filed")]
        public int ExpenseCategoryID { get; set; }
        [Required(ErrorMessage = "Empty Filed")]
        public Decimal Amount { get; set; }
        [Required]
        public DateTime ExpenseDate { get; set; }

        public string Note { get; set; }
        public IncomeCategory ExpenseCategory { get; set; }
    }
}
