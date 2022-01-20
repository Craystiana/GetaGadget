using System;

namespace GetaGadget.Domain.DTO.Order
{
    public class PlaceOrderModel
    {
        public string County { get; set; }

        public string City { get; set; }

        public string FullAddress { get; set; }

        public string PostalCode { get; set; }

        public string CardNumber { get; set; }

        public int? CardCsv { get; set; }

        public DateTime? CardExpirationDate { get; set; }

        public int? DeliveryMethodId { get; set; }

        public string Coupon { get; set; }
    }
}
