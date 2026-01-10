using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinanceTracker.Models
{
    public class Income
    {
        [Key]
        public int IncomeID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required(ErrorMessage ="Empty Filed")]
        public int IncomeCategoryID { get; set; }
        [Required(ErrorMessage = "Empty Filed")]
        public Decimal Amount { get; set; }
        [Required]
        public DateTime IncomeDate {  get; set; }

        public  string Note {  get; set; }
    }
}
