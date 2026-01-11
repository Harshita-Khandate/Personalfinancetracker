using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTracker.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [DisplayName("Enter your Name")]
        [Required(ErrorMessage ="Name is Empty")]

        public string Name { get; set; }


        [Required(ErrorMessage = "Email is Empty")]
        [EmailAddress]
        public string Email {  get; set; }


        [Required(ErrorMessage = "Password is Empty")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%?&])[A-Za-z\d@$!%&?]{8,}$",ErrorMessage ="Passwprd must be at least 8 characters and contain 1 uppercase letter,1 number and 1 special chaeacter ")]
        public string Password { get; set; }
    }
}
