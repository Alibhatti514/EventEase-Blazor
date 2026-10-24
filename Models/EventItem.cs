using System.ComponentModel.DataAnnotations;
namespace EventEase.Models;
public sealed class EventItem
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Event name is required")][StringLength(100, MinimumLength = 3)] public string Name { get; set; } = "";
    [Required][StringLength(120)] public string Location { get; set; } = "";
    [Required] public DateTime Date { get; set; }
    [StringLength(500)] public string Description { get; set; } = "";
    public EventItem Copy() => new() { Id = Id, Name = Name, Location = Location, Date = Date, Description = Description };
}
