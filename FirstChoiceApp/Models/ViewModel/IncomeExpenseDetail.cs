using System;
using System.ComponentModel.DataAnnotations;

namespace FirstChoiceApp.Models.ViewModel
{
    public class IncomeExpenseDetail
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-M-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime D_Date { get; set; }
        public decimal Earning { get; set; }
        public decimal Expense { get; set; }
        public decimal Balance { get; set; }
    }
}