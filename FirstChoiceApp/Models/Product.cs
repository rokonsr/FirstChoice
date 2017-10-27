using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.ModelBinding;

namespace FirstChoiceApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        [ForeignKey("Id")]
        public virtual Brand Brand { get; set; }

        public string BrandName { get; set; }

        [Required(ErrorMessage = "Item is required")]
        [Display(Name = "Item Name")]
        public int ItemId { get; set; }
        [ForeignKey("Id")]
        public virtual Item Item { get; set; }

        public string ItemName { get; set; }

        [Required(ErrorMessage = "Measurement is required")]
        [Display(Name = "Measurement")]
        public int MeasurementId { get; set; }
        [ForeignKey("Id")]
        public virtual Measurement Measurement { get; set; }

        public string MeasurementName { get; set; }

        [Display(Name = "Product Size")]
        public int SizeId { get; set; }
        [ForeignKey("Id")]
        public virtual Size Size { get; set; }

        public string ProductSize { get; set; }

        [Display(Name = "Product Type")]
        public int TypeId { get; set; }
        [ForeignKey("Id")]
        public virtual ProductType ProductType { get; set; }

        public string TypeName { get; set; }

        public decimal Stock { get; set; }
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product code is required")]
        [Display(Name = "Product Code")]
        [StringLength(50, ErrorMessage = "Product code should be maximum 50 characters long", MinimumLength = 3)]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [Display(Name = "Product Name")]
        [StringLength(200, ErrorMessage = "Product name should be maximum 200 characters long", MinimumLength = 3)]
        public string ProductName { get; set; }

        public string InvoiceNo { get; set; }
    }
}