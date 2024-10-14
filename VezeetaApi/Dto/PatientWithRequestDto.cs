namespace VezeetaApi.Dto
{
    public class PatientWithRequestDto
    {
        public PatientDto Detailes { get; set; }
        public List<RequestDto> Requests { get; set; }
    }
}
