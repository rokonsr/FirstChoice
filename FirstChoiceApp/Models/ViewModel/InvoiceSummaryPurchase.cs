namespace FirstChoiceApp.Models.ViewModel
{
    public class InvoiceSummaryPurchase
    {
        public string SupplierName { get; set; }
        public string InvoiceNo { get; set; }
        public string PurchaseDate { get; set; }
        public string ProductName { get; set; }
        public string PurchaseType { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}