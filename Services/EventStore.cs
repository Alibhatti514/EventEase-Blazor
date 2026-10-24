using System.ComponentModel.DataAnnotations;
using EventEase.Models;
namespace EventEase.Services;
public sealed class EventStore
{
    private readonly object gate = new();
    private readonly List<EventItem> events = new()
    {
        new() { Id=1, Name="Innovation Summit", Location="Lahore Convention Center", Date=new DateTime(2027,2,12,9,0,0), Description="A day of technology talks, workshops and networking." },
        new() { Id=2, Name="Design & Product Meetup", Location="Karachi Arts Council", Date=new DateTime(2027,3,8,14,0,0), Description="Meet designers, founders and product teams." },
        new() { Id=3, Name="Community Leadership Forum", Location="Multan Business Hub", Date=new DateTime(2027,4,20,10,0,0), Description="Build connections and exchange practical ideas." },
        new() { Id=4, Name="Future of Work Conference", Location="Islamabad Expo Hall", Date=new DateTime(2027,5,6,11,0,0), Description="Explore collaboration and emerging workplace tools." },
        new() { Id=5, Name="Startup Networking Night", Location="Lahore Innovation Lab", Date=new DateTime(2027,6,18,18,0,0), Description="Connect with entrepreneurs and mentors." }
    };
    public IReadOnlyList<EventItem> Search(string? query, int page, int pageSize, out int total)
    {
        lock(gate) { var filtered=events.Where(e=>string.IsNullOrWhiteSpace(query)||e.Name.Contains(query,StringComparison.OrdinalIgnoreCase)||e.Location.Contains(query,StringComparison.OrdinalIgnoreCase)).OrderBy(e=>e.Date).ToList(); total=filtered.Count; return filtered.Skip(Math.Max(0,page-1)*pageSize).Take(pageSize).Select(e=>e.Copy()).ToList(); }
    }
    public EventItem? Get(int id) { lock(gate) return events.FirstOrDefault(e=>e.Id==id)?.Copy(); }
    public bool Update(EventItem item)
    {
        if (!Validator.TryValidateObject(item,new ValidationContext(item),new List<ValidationResult>(),true) || item.Date == default) return false;
        lock(gate) { var existing=events.FirstOrDefault(e=>e.Id==item.Id); if(existing is null) return false; existing.Name=item.Name.Trim(); existing.Location=item.Location.Trim(); existing.Date=item.Date; existing.Description=item.Description.Trim(); return true; }
    }
}
