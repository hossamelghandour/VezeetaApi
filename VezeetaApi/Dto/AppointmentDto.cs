using VezeetaApi.Models;

namespace VezeetaApi.Dto
{
    public class AppointmentDto
    {
        public string DoctorId { get; set; }
        public decimal Price { get; set; }
        public Times times { get; set; }
        public Days days { get; set; }

    }
}
