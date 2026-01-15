using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceTracker.Models
{
    [Table("ExpenseCategory")]
    public class ExpenseCategory
    {
        [Key]
        public int ExpenseCategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
