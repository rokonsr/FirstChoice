namespace FirstChoiceApp.Models
{
    public class PurchaseDetail
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal SaleRate { get; set; }
        public decimal Quantity { get; set; }

        public int PurchaseType { get; set; }
        public string ProductName { get; set; }
    }
}