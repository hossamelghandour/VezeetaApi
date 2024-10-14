using VezeetaApi.Dto;

namespace VezeetaApi.Repository.AppointmentRepo
{
    public interface IAppointmentRepo
    {
        void Add(AppointmentDto item);
        void Update(int id,TimeSpan item);
        void Delete(int AppointmentId, TimeSpan timeToDelete);
    }
}
