namespace GetaGadget.Domain.DTO.Product
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public int Stock { get; set; }

        public string Provider { get; set; }

        public string DeliveryMethod { get; set; }

        public string Category { get; set; }

        public string Photo { get; set; }
    }
}
