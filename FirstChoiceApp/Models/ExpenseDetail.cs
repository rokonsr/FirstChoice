using System;

namespace FirstChoiceApp.Models
{
    public class ExpenseDetail
    {
        public int Id { get; set; }
        public int ExpenseTypeId { get; set; }
        public decimal ExpenseAmount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string Remarks { get; set; }
    }
}