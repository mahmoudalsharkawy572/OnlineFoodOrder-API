namespace Shared.OrderModels
{
    public record OrderRequest
    {
        public string BasketId { get; set; }
        public AddressDTO ShipppingAddress { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}
