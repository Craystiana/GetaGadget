using System.ComponentModel.DataAnnotations;

namespace GetaGadget.Domain.Entities
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }

        public string Code { get; set; }

        public  int MinOrderValue { get; set; }

        public int Discount { get; set; }
    }
}
