using VezeetaApi.Dto;

namespace VezeetaApi.Repository.RequestRepo
{
    public interface IRequestRepo
    {
        bool HasRequests(string doctorId);
        RequestCountDto NumOfRequests();
        List<RequestPatientDto> Bookingofboctor(int pageSize, int pageNumber);
        List<RequestDto> BookingofPatient();
        void CancelBooking(int id);

        void ADDRequest(AddRequestDto item);
        void confirmcheckup(int id);
    }
}
