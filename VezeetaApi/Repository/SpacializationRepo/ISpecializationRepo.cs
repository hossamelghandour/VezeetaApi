using VezeetaApi.Dto;
using VezeetaApi.Models;

namespace VezeetaApi.Repository.SpacializationRepo
{
    public interface ISpecializationRepo
    {
        Specialization GetById(int id);
        void Add(SpecializationDto item);
    }
}
