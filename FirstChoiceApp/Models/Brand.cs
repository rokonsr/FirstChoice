using System.ComponentModel.DataAnnotations;

namespace FirstChoiceApp.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        [Display(Name = "Brand Name")]
        [StringLength(100, ErrorMessage = "Brand should be 2 to 100 characters long", MinimumLength = 2)]
        public string BrandName { get; set; }
    }
}