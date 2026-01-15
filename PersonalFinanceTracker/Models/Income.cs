using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceTracker.Models
{
    [Table("Income")]
    public class Income
    {
        [Key]
        public int IncomeID { get; set; }

        public int UserID { get; set; }
        public int IncomeCategoryID { get; set; }

        public decimal Amount { get; set; }
        public DateTime IncomeDate { get; set; } = DateTime.Now;

        public string Note { get; set; }

        public IncomeCategory IncomeCategory { get; set; }
    }

}