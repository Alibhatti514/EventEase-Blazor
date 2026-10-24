using EventEase.Models;
namespace EventEase.Services;
public sealed class RegistrationStore
{
    private readonly object gate = new();
    private readonly List<Registration> registrations = new();
    public bool Add(Registration item)
    {
        if (!ValidatorValid(item)) return false;
        lock(gate) { if (registrations.Any(r=>r.EventId==item.EventId && r.Email.Equals(item.Email,StringComparison.OrdinalIgnoreCase))) return false; registrations.Add(new Registration { EventId=item.EventId, FullName=item.FullName.Trim(), Email=item.Email.Trim(), AttendanceType=item.AttendanceType, RegisteredAtUtc=DateTime.UtcNow }); return true; }
    }
    private static bool ValidatorValid(Registration item) => System.ComponentModel.DataAnnotations.Validator.TryValidateObject(item,new System.ComponentModel.DataAnnotations.ValidationContext(item),new List<System.ComponentModel.DataAnnotations.ValidationResult>(),true) && item.EventId>0;
    public IReadOnlyList<Registration> All() { lock(gate) return registrations.Select(r=>new Registration {EventId=r.EventId, FullName=r.FullName, Email=r.Email, AttendanceType=r.AttendanceType, RegisteredAtUtc=r.RegisteredAtUtc}).ToList(); }
}
