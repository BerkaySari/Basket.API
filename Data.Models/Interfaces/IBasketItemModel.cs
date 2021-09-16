namespace Data.Models.Interfaces
{
    public interface IBasketItemModel<TKey>
    {
        public TKey ProductId { get; set; }
        public string ProductName { get; set; }
        public string Supplier { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string DeliveryDateRange { get; set; }
        public string Color { get; set; }
    }
}
