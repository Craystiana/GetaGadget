using System.Collections.Generic;

namespace GetaGadget.Domain.DTO.Product
{
    public class ProductQueryModel
    {
        public IEnumerable<int> ProviderIds { get; set; }

        public IEnumerable<int> DeliveryMethodIds { get; set; }

        public IEnumerable<int> CategoryIds { get; set; }

        public bool? InStock { get; set; }

        public int? SortById { get; set; }

        public string SearchTerm { get; set; }
    }
}
