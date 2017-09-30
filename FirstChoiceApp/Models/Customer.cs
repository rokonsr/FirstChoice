using System.ComponentModel.DataAnnotations;

namespace FirstChoiceApp.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer name is required")]
        [Display(Name = "Customer Name")]
        [StringLength(100, ErrorMessage = "Customer name should be 5 to 100 characters long", MinimumLength = 5)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [Display(Name = "Contact Number")]
        [StringLength(15, ErrorMessage = "Number should be 5 to 15 characters long", MinimumLength = 5)]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Please input valid email address")]
        [StringLength(50, ErrorMessage = "Email should be maximum 50 characters long", MinimumLength = 5)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(300, ErrorMessage = "Address should be 5 to 300 characters long", MinimumLength = 5)]
        public string Address { get; set; }
    }
}