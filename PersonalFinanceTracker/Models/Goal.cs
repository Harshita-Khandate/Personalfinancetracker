using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PersonalFinanceTracker.Models
{
    public class Goal
    {
        //        create table Goal(GoalID int primary key identity(1,1),UserID int ,
        //foreign Key(UserID) references Users(UserID),GoalName varchar(20),
        //TargetAmount float,SavedAmount float Default 0, Deadline date,
        //CreatedDate date default GETDATE());
        [Key]
        public int GoalID {  get; set; }
        public int UserID { get; set; }
        [Required]
        public string GoalName { get; set; }
        [Required]
       public decimal TargetAmount { get; set; }
        [Required]
        public decimal SavedAmount { get; set; }
        [Required]
        public DateOnly Deadline { get; set; }
        [Required]
        public DateOnly CreatedDate { get; set; }

    }
}
