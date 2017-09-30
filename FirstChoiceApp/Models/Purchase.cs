using System;
using System.ComponentModel.DataAnnotations;

namespace FirstChoiceApp.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public int SupplierId { get; set; }

        public string InvoiceNo { get; set; }

        [Required(ErrorMessage = "Required")]
        //[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/M/yy}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "Required")]
        public decimal TotalAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        [Required(ErrorMessage = "Required")]
        public int PurchaseType { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}