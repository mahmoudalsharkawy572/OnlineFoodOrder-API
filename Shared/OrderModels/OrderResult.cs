namespace Shared.OrderModels
{
    public record OrderResult
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public AddressDTO ShippingAddress { get; set; }
        public ICollection<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
        public string PaymentStatus { get; set; } 
        public string DeliveryMethod { get; set; } 
        public decimal Subtotal { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public decimal Total { get; set; }
    }
}
