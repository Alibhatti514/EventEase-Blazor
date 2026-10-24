using System.ComponentModel.DataAnnotations;
namespace EventEase.Models;
public sealed class Registration
{
    public int EventId { get; set; }
    [Required(ErrorMessage="Please enter your name")][StringLength(80, MinimumLength=2)] public string FullName { get; set; } = "";
    [Required][EmailAddress] public string Email { get; set; } = "";
    [Required(ErrorMessage="Choose an attendance type")] public string AttendanceType { get; set; } = "In person";
    public DateTime RegisteredAtUtc { get; set; } = DateTime.UtcNow;
}
