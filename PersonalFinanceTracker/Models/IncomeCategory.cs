using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Models
{
    public class IncomeCategory
    {
        [Key]
        public int IncomeCategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public List<Income> Incomes { get; set; }
    }
}
