using Microsoft.EntityFrameworkCore;
using VezeetaApi.Data;
using VezeetaApi.Dto;
using VezeetaApi.Models;

namespace VezeetaApi.Repository.PatientRepo
{
    public class PatientRepo:IPatientRepo
    {
        private readonly AppDbContext myContext;

        public PatientRepo(AppDbContext myContext)
        {
            this.myContext = myContext;
        }

        public List<PatientDto> GetAll()
        {
            var patient = myContext.Users
           .Where(d => d.Type == AccountType.Patient)
           .Select(d => new PatientDto
           {
               Image = d.Image,
               FullName = d.FullName,
               Email = d.Email,
               Phone = d.PhoneNumber,
               Gender = (Gender)d.Gender,
               DateOfBirth = d.DateofBirth,
           })
           .ToList();

            return patient;
        }

        public PatientWithRequestDto GetById(string id)
        {
            var patientDetails = myContext.Users
             .Include(p => p.requests)
                 .ThenInclude(r => r.doctor)
                     .ThenInclude(d => d.applicationUser)
             .Include(p => p.requests)
                 .ThenInclude(r => r.doctor)
                     .ThenInclude(d => d.specialization)
             .Include(p => p.requests)
                 .ThenInclude(r => r.Appointment)
                     .ThenInclude(a => a.times)
             .Include(p => p.requests)
                 .ThenInclude(r => r.DiscountCode)
             .FirstOrDefault(p => p.Id == id);

            if (patientDetails == null)
            {
                return null;
            }

            var patientRequests = patientDetails.requests
                .Where(r => r.doctor != null
                            && r.Appointment != null
                            && r.Appointment.times != null
                            && r.DiscountCode != null)
                .Select(r => new RequestDto
                {
                    DoctorImage = r.doctor?.applicationUser?.Image ?? string.Empty,
                    DoctorName = r.doctor?.applicationUser?.FullName ?? string.Empty,
                    SpecializationName = r.doctor?.specialization?.NameEn ?? string.Empty,
                    day = r.Appointment.Days,
                    time = r.Appointment.times.Select(t => t.Time),
                    FinalPrice = r.FinalPrice,
                    Status = r.Status,
                    DiscoundCode = r.DiscountCode?.Code ?? string.Empty
                })
                .ToList();

            var response = new PatientWithRequestDto
            {
                Detailes = new PatientDto
                {
                    Image = patientDetails?.Image ?? string.Empty,
                    FullName = patientDetails?.FullName ?? string.Empty,
                    Email = patientDetails?.Email ?? string.Empty,
                    Phone = patientDetails?.PhoneNumber ?? string.Empty,
                    Gender = patientDetails?.Gender ?? Gender.Female,
                    DateOfBirth = patientDetails?.DateofBirth ?? DateTime.MinValue
                },
                Requests = patientRequests,
            };

            return response;
        }

        public int NumsOfPatient()
        {
            var patient = myContext.Users.Where(d => d.Type == AccountType.Patient).Count();
            return patient;
        }
    }
}
