using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceTracker.Models
{
    public class Income
    {
        [Key]
        public int IncomeID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime IncomeDate { get; set; }

        [Required]
        public int IncomeCategoryID { get; set; }
        public string Note { get; set; }

        // ❌ DO NOT mark Required
        public int UserID { get; set; }

        [ForeignKey("IncomeCategoryID")]
        public IncomeCategory IncomeCategory { get; set; }
    }
}
