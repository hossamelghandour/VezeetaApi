using VezeetaApi.Dto;

namespace VezeetaApi.Repository.DoctorRepo
{
    public interface IDoctorRepo
    {
        List<DoctorDto> SearchOnDoctors(int pageNumber, int pageSize, string search);

        List<DoctorWithSpecializationDto>GetAll(int pageNumber, int pageSize, string search);
        DoctorWithSpecializationDto GetById(string id);
        List<TopDoctorDto> Top10Doctor();
        List<Top5SpecializeDto> Top5Specialize();
        void Update(string id, DoctorWithSpecializationDto item);
        void Delete(string id);
        int NumberOfDoctor();

    }
}
