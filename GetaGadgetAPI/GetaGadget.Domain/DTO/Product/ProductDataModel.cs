using GetaGadget.Domain.DTO.Generic;
using System.Collections.Generic;

namespace GetaGadget.Domain.DTO.Product
{
    public class ProductDataModel
    {
        public IEnumerable<GenericModel> Providers { get; set; }

        public IEnumerable<GenericModel> DeliveryMethods { get; set; }

        public IEnumerable<GenericModel> Categories { get; set; }
    }
}
