using System.ComponentModel.DataAnnotations.Schema;
using VezeetaApi.Models;

namespace VezeetaApi.Dto
{
    public class RequestDto
    {
        public string? DoctorImage { get; set; }
        public string? DoctorName { get; set; }
        public string? SpecializationName { get; set; }
        public Days? day { get; set; }
        public IEnumerable<TimeSpan>? time { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? FinalPrice { get; set; }
        public RequestStatus? Status { get; set; }
        public string? DiscoundCode { get; set; }
    }
}
