using System.ComponentModel.DataAnnotations;

namespace FirstChoiceApp.Models
{
    public class ExpenseType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Expense title is required")]
        [Display(Name = "Expense Title")]
        [StringLength(100, ErrorMessage = "Expense title should be 3 to 100 characters long", MinimumLength = 3)]
        public string ExpenseTitle { get; set; }

        [StringLength(200, ErrorMessage = "Remarks should be 3 to 200 characters long", MinimumLength = 3)]
        public string Remarks { get; set; }
    }
}