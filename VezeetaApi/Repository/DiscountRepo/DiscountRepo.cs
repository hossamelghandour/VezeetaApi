using Microsoft.EntityFrameworkCore;
using VezeetaApi.Data;
using VezeetaApi.Dto;
using VezeetaApi.Models;

namespace VezeetaApi.Repository.DiscountRepo
{
    public class DiscountRepo : IDiscountRepo
    {
        private readonly AppDbContext myContext;

        public DiscountRepo(AppDbContext myContext)
        {
            this.myContext = myContext;
        }

        public void Add(DiscountDto item)
        {

            var discountCode = new DiscountCode
            {
                Code = item.DiscountCode,
                DiscountType = item.Discounttype,
                Value = item.Value,
                // CompletedRequests = item.ComletedREquested

            };


            myContext.DiscountCodes.Add(discountCode);
            myContext.SaveChanges();
        }

        public void DeActivated(int id)
        {
            var code = myContext.DiscountCodes.FirstOrDefault(d => d.DiscountCodeId == id);
            if (code != null)
            {
                code.IsActive = false;
                myContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            DiscountCode code = myContext.DiscountCodes.FirstOrDefault(d => d.DiscountCodeId == id);
            myContext.Remove(code);
            myContext.SaveChanges();
        }

        public void Update(int id, DiscountDto item)
        {
            DiscountCode discountCode = myContext.DiscountCodes
            .FirstOrDefault(dc => dc.DiscountCodeId == id);

            if (discountCode != null && discountCode.Request == null)
            {

                discountCode.Code = item.DiscountCode;
                discountCode.DiscountType = item.Discounttype;
                discountCode.Value = item.Value;
                discountCode.CompletedRequest = item.ComletedREquested;

                myContext.SaveChanges();
            }
        }
    }
}
