using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PersonalFinanceTracker.Models
{
    [Table("IncomeCategory")]
    public class IncomeCategory
    {
        [Key]
        public int IncomeCategoryID { get; set; }
        public string CategoryName { get; set; }
    }


}
