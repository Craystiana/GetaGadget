namespace GetaGadget.Domain.DTO.Order
{
    public class OrderHistoryModel : OrderModel
    {
        public string Address { get; set; }
        public string CardDetails { get; set; }
        public string DeliveryMethod { get; set; }
    }
}
