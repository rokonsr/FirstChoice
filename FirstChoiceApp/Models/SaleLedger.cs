using System;
using System.ComponentModel.DataAnnotations;

namespace FirstChoiceApp.Models
{
    public class SaleLedger
    {
        public int Id { get; set; }

        public int SaleId { get; set; }

        [Display(Name = "Sale Amount")]
        public decimal SaleAmount { get; set; }

        [Display(Name = "Paid Amount")]
        public decimal PaidAmount { get; set; }

        public decimal Balance { get; set; }

        public DateTime CreatedDate { get; set; }

        public int TransactionType { get; set; }

        public string Remarks { get; set; }


        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Customer")]
        public string CustomerName { get; set; }

        public string Status { get; set; }

        [Display(Name = "Due Amount")]
        public decimal DueAmount { get; set; }

        public DateTime PaidDate { get; set; }
    }
}