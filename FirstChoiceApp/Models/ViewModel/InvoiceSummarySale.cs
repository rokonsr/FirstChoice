namespace FirstChoiceApp.Models.ViewModel
{
    public class InvoiceSummarySale
    {
        public string CustomerName { get; set; }
        public string InvoiceNo { get; set; }
        public string SaleDate { get; set; }
        public string ProductName { get; set; }
        public string SaleType { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}