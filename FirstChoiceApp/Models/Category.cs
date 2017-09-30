using System.ComponentModel.DataAnnotations;

namespace FirstChoiceApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Category name is required")]
        [Display(Name = "Category Name")]
        [StringLength(100, ErrorMessage = "Category should be 3 to 100 characters long", MinimumLength = 3)]
        public string CategoryName { get; set; }
    }
}