namespace GetaGadget.Domain.DTO.Order
{
    public class OrderProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Photo { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}
