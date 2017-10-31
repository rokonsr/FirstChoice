using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstChoiceApp.Models
{
    public class ExpenseDetail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Expense title is required")]
        [Display(Name = "Expense Title")]
        public int ExpenseTypeId { get; set; }
        [ForeignKey("Id")]
        public virtual ExpenseType ExpenseType { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Display(Name = "Amount")]
        public decimal ExpenseAmount { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/M/yy}", ApplyFormatInEditMode = true)]
        public DateTime ExpenseDate { get; set; }

        public string Remarks { get; set; }


        public string ExpenseTitle { get; set; }
    }
}