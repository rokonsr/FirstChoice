using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstChoiceApp.Models
{
    public class OtherIncomeDetail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Income type is required")]
        [Display(Name = "Income Type")]
        public int IncomeTypeId { get; set; }
        [ForeignKey("Id")]

        [Required(ErrorMessage = "Amount is required")]
        [Display(Name = "Income Amount")]
        public decimal IncomeAmount { get; set; }

        public string Remarks { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Created Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Income Type")]
        public string IncomeTypeTitle { get; set; }
    }
}