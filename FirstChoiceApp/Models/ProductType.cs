using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstChoiceApp.Models
{
    public class ProductType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Item is required")]
        [Display(Name = "Item Name")]
        public int ItemId { get; set; }
        [ForeignKey("Id")]
        public virtual Item Item { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [Display(Name = "Product Type")]
        [StringLength(100, ErrorMessage = "Product Type should be 1 to 50 characters long", MinimumLength = 1)]
        public string TypeName { get; set; }

        public string ItemName { get; set; }
    }
}