using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceTracker.Models
{
    [Table("Expense")]
    public class Expense
    {
        [Key]
        public int ExpenseID { get; set; }
   
        public int UserID { get; set; }

        public int ExpenseCategoryID { get; set; }
        [Required]
        public decimal Amount { get; set; }
       
        public DateTime ExpenseDate { get; set; } = DateTime.Now;

        public string Note { get; set; }

        public ExpenseCategory ExpenseCategory { get; set; }
    }
}
