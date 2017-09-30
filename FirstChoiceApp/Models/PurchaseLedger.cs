using System;
using System.ComponentModel.DataAnnotations;

namespace FirstChoiceApp.Models
{
    public class PurchaseLedger
    {
        public int Id { get; set; }

        public int PurchaseId { get; set; }

        [Display(Name = "Purchase Amount")]
        public decimal PurchaseAmount { get; set; }

        [Display(Name = "Paid Amount")]
        public decimal PaidAmount { get; set; }

        public decimal Balance { get; set; }

        public DateTime CreatedDate { get; set; }

        public int TransactionType { get; set; }

        public string Remarks { get; set; }


        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        [Display(Name = "Supplier")]
        public string SupplierName { get; set; }

        public string Status { get; set; }

        [Display(Name = "Due Amount")]
        public decimal DueAmount { get; set; }

        public DateTime PaidDate { get; set; }
    }
}