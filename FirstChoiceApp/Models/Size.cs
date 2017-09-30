using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstChoiceApp.Models
{
    public class Size
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Item is required")]
        [Display(Name = "Item Name")]
        public int ItemId { get; set; }
        [ForeignKey("Id")]
        public virtual Item Item { get; set; }

        [Required(ErrorMessage = "Size is required")]
        [StringLength(30, ErrorMessage = "Size should be 5 to 30 characters long", MinimumLength = 2)]
        [Display(Name = "Product Size")]
        public string ProductSize { get; set; }

        public string ItemName { get; set; }
    }
}