using VezeetaApi.Models;

namespace VezeetaApi.Dto
{
    public class DiscountDto
    {
        public string DiscountCode { get; set; }
        public DiscountType Discounttype { get; set; }
        public decimal Value { get; set; }

        public int ComletedREquested
        {
            get; set;
        }
    }
}
