using Microsoft.EntityFrameworkCore;
using VezeetaApi.Data;
using VezeetaApi.Dto;

namespace VezeetaApi.Repository.RequestRepo
{
    public class RequestRepo : IRequestRepo
    {
        private readonly AppDbContext _context;

        public RequestRepo(AppDbContext context)
        {
            _context = context;
        }

        public void ADDRequest(AddRequestDto item)
        {
            throw new NotImplementedException();
        }

        public List<RequestPatientDto> Bookingofboctor(int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public List<RequestDto> BookingofPatient()
        {
            throw new NotImplementedException();
        }

        public void CancelBooking(int id)
        {
            throw new NotImplementedException();
        }

        public void confirmcheckup(int id)
        {
            throw new NotImplementedException();
        }

        public bool HasRequests(string doctorId)
        {
            bool hasRequests = _context.Requests.Any(r => r.DoctorId == doctorId);

            return hasRequests;
        }

        public RequestCountDto NumOfRequests()
        {
            throw new NotImplementedException();
        }
    }
}
