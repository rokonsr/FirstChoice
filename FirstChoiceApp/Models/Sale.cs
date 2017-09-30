using System;
using System.ComponentModel.DataAnnotations;

namespace FirstChoiceApp.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public int CustomerId { get; set; }
                
        public DateTime SaleDate { get; set; }

        [Required(ErrorMessage = "Required")]
        public decimal TotalAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        [Required(ErrorMessage = "Required")]
        public int SaleType { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}