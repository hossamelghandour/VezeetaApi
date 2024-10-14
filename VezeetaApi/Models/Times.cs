namespace VezeetaApi.Models
{
    public class Times
    {
        public int TimesId { get; set; }
        public TimeSpan Time { get; set; }
        public int AppointmentId { get; set; }
        public virtual Appointment? Appointments { get; set; }
    }
}
