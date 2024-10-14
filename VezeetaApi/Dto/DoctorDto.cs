using VezeetaApi.Models;

namespace VezeetaApi.Dto
{
    public class DoctorDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public Gender? Gender{ get; set; }
        public string? Phone { get; set; }
        public string? SpeciaizationNameEN { get; set; }
        public string? SpeciaizationNameAR { get; set; }
        public decimal? Price { get; set; }
        public IEnumerable<Appointment>? Appointments { get; set; } 

    }
}
