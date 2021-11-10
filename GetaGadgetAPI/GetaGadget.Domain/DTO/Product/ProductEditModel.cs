namespace GetaGadget.Domain.DTO.Product
{
    public class ProductEditModel
    {
        public int? ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public int Stock { get; set; }

        public int Provider { get; set; }

        public int DeliveryMethod { get; set; }

        public int Category { get; set; }

        public string Photo { get; set; }
    }
}
