using VezeetaApi.Models;

namespace VezeetaApi.Dto
{
    public class DoctorWithSpecializationDto
    {
        public string? Image { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Password { get; set; }
        public int? SpecializationId { get; set; }
        public string? SpecializationNameEN { get; set; }
        public string? SpecializationNameAR { get; set; }
    }
}
