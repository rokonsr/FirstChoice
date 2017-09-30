using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstChoiceApp.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Item name is required")]
        [Display(Name = "Item Name")]
        [StringLength(50, ErrorMessage = "Category should be 2 to 50 characters long", MinimumLength = 2)]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("Id")]
        public virtual Category Category { get; set; }

        public string CategoryName { get; set; }
    }
}