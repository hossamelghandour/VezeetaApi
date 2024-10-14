using VezeetaApi.Dto;

namespace VezeetaApi.Repository.PatientRepo
{
    public interface IPatientRepo
    {
        List<PatientDto> GetAll();
        PatientWithRequestDto GetById(string id);
        int NumsOfPatient();
    }
}
