using VezeetaApi.Dto;

namespace VezeetaApi.Repository.DiscountRepo
{
    public interface IDiscountRepo
    {
        void Add(DiscountDto item);
        void Update(int id, DiscountDto item);
        void Delete(int id);
        void DeActivated(int id);
    }
}
