using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceApp.Models.ViewModel
{
    public class IncomeExpenseDetail
    {
        public string D_Date { get; set; }
        public decimal Earning { get; set; }
        public decimal Expense { get; set; }
        public decimal Balance { get; set; }
    }
}