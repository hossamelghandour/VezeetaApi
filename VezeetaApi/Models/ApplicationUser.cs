using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VezeetaApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountType
    {
        Admin,
        Patient,
        Doctor
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Female = 0,
        Male = 1
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DiscountType
    {
        Percentage,
        Value
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RequestStatus
    {
        Pending,
        Completed,
        Cancelled
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Days
    {
        Saturday,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday
    }

    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Image { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateofBirth { get; set; }
        public AccountType Type { get; set; }

        [ForeignKey("Doctor")]
        public Doctor? DoctorProfile { get; set; }
        public string? RefreshToken { get; set; }

        public ICollection<Request>? requests { get; set; }

        public bool? IsDeleted { get; set; } = false;

    }

    public class ApplicationRole : IdentityRole<string>
    {
        public string? Date { get; set; }

    }
}
