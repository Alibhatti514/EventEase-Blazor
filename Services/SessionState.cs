namespace EventEase.Services;
// Scoped to the interactive Blazor Server circuit. Resets on a new circuit/browser refresh.
public sealed class SessionState
{
    public string? DisplayName { get; set; }
    public HashSet<int> VisitedEventIds { get; } = new();
    public HashSet<int> RegisteredEventIds { get; } = new();
}
