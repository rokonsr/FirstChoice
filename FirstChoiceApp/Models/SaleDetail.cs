namespace FirstChoiceApp.Models
{
    public class SaleDetail
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal SaleRate { get; set; }
        public decimal Quantity { get; set; }

        public int SaleType { get; set; }
        public string ProductName { get; set; }
    }
}