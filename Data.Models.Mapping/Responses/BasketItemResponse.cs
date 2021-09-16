﻿namespace Data.Models.Mapping.Responses
{
    public class BasketItemResponse : BaseResponse
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Supplier { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string DeliveryDateRange { get; set; }
        public string Color { get; set; }
    }
}
